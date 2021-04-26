using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Alpha.Web.API.Security.Helpers
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IValidator<UserModel> _userModelValidator;
        private readonly IAspNetUsersService _userService;

        public AccountService(
            SignInManager<AspNetUser> signInManager,
            IValidator<UserModel> userModelValidator,
            IAspNetUsersService userService)
        {
            _signInManager = signInManager;
            _userModelValidator = userModelValidator;
            _userService = userService;
        }

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            var user = await _userService.GetByEmail(model.Email);
            return await _signInManager.PasswordSignInAsync(user.Name, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> ValidatePasswordAsync(UserModel userModel, string password)
        {
            _userModelValidator.ValidateAndThrow(userModel);

            var userEntity = UserModel.FillUp(userModel);

            return await _signInManager.PasswordSignInAsync(userEntity, password, true, false);
        }
    }
}
