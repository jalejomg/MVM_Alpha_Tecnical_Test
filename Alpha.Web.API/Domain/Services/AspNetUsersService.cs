using Alpha.Tests.Util;
using Alpha.Web.API.Constants;
using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public class AspNetUsersService : IAspNetUsersService
    {
        /// <summary>
        /// This class contain the core of acctions about user domain.
        /// </summary>
        private IAspNetUsersRepository _usersRepository;
        public IValidator<UserModel> _userModelValidator;
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        public AspNetUsersService(
            IAspNetUsersRepository usersRepository,
            IValidator<UserModel> userModelValidator,
            UserManager<AspNetUser> userManager,
            RoleManager<AspNetRole> roleManager)
        {
            _usersRepository = usersRepository;
            _userModelValidator = userModelValidator;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserModel> GetByIdAsync(string userId)
        {
            var userEntity = await _usersRepository.GetByIdAsync(userId);
            if (userEntity == null) throw new NotFoundCustomException("User was not found");
            if (!userEntity.State) throw new NotFoundCustomException("User was not found");

            return UserModel.MakeOne(userEntity);
        }

        public async Task<UserModel> GetByEmail(string email)
        {
            var userEntity = await _userManager.FindByEmailAsync(email);

            if (userEntity == null) throw new NotFoundCustomException("User was not found");

            return UserModel.MakeOne(userEntity);
        }

        public async Task<ResponseModel<IEnumerable<UserModel>>> ListAsync()
        {
            var userEntities = await _usersRepository.GetAllAsync();

            return new ResponseModel<IEnumerable<UserModel>>
            {
                Count = userEntities.Count(),
                Data = UserModel.MakeMany(userEntities.Where(userEntity => userEntity.State))
            };
        }

        public async Task<IdentityResult> AddAsync(UserModel userModel, string password)
        {
            _userModelValidator.ValidateAndThrow(userModel);

            var userEntity = UserModel.FillUp(userModel);

            userEntity.Id = UtilRandomGenerator.GenerateString(10); ;

            return await _userManager.CreateAsync(userEntity, password);
        }

        public async Task<string> UpdateAsync(string userId, UserModel userModel)
        {
            await _userModelValidator.ValidateAndThrowAsync(userModel);

            var userEntity = await _usersRepository.GetByIdAsync(userId);

            if (userEntity == null) throw new NotFoundCustomException("User was not found");

            userEntity = UserModel.FillUp(userModel);

            await _usersRepository.UpdateAsync(userEntity);

            return userEntity.Id;
        }

        public async Task AddUserToRoleAsync(UserModel userModel, int roleCode)
        {
            _userModelValidator.ValidateAndThrow(userModel);

            var roleName = GetRoleName(roleCode);

            var userEntity = UserModel.FillUp(userModel);

            await _userManager.AddToRoleAsync(userEntity, roleName);
        }

        public async Task CheckRoleAsync(int roleCode)
        {
            var roleName = GetRoleName(roleCode);
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new AspNetRole
                {
                    Name = roleName
                });
            }
        }

        public async Task<bool> IsUserInRoleAsync(UserModel userModel, int roleCode)
        {
            _userModelValidator.ValidateAndThrow(userModel);

            var roleName = GetRoleName(roleCode);

            var userEntity = UserModel.FillUp(userModel);

            return await _userManager.IsInRoleAsync(userEntity, roleName);
        }



        private string GetRoleName(int roleCode)
        {
            return
                roleCode == UserRoles.AddresseeCode ?
                UserRoles.Addressee
                : roleCode == UserRoles.ContentManagerCode ?
                 UserRoles.ContentManager
                : roleCode == UserRoles.AdminCode ?
                UserRoles.Admin
                : UserRoles.Addressee;
        }

        public async Task DeleteAsync(string userId)
        {
            var userEntity = await _usersRepository.GetByIdAsync(userId);

            if (userEntity == null) throw new NotFoundCustomException("User was not found");
            if (!userEntity.State) throw new NotFoundCustomException("User was not found");

            userEntity.State = EntityStatus.DeletedValue;

            await _userManager.UpdateAsync(userEntity);
        }
    }
}
