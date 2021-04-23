using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;
        public IValidator<UserModel> _userModelValidator;
        public UsersService(
            IUsersRepository usersRepository,
            IValidator<UserModel> userModelValidator)
        {
            _usersRepository = usersRepository;
            _userModelValidator = userModelValidator;
        }

        public async Task<UserModel> GetByIdAsync(string userId)
        {
            var userEntity = await _usersRepository.GetByIdAsync(userId);

            if (userEntity == null) throw new NotFoundCustomException("User was not found");
            if (!userEntity.State) throw new NotFoundCustomException("User was not found");

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

        public async Task<string> CreateAsync(UserModel userModel)
        {
            await _userModelValidator.ValidateAndThrowAsync(userModel);

            var userEntity = UserModel.FillUp(userModel);

            userEntity.State = true;

            await _usersRepository.CreateAsync(userEntity);

            return userEntity.Id;
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

        public async Task DeleteAsync(string userId)
        {
            var userEntity = await _usersRepository.GetByIdAsync(userId);

            if (userEntity == null) throw new NotFoundCustomException("User was not found");
            if (!userEntity.State) throw new NotFoundCustomException("User was not found");
            userEntity.State = false;

            await _usersRepository.UpdateAsync(userEntity);
        }
    }
}
