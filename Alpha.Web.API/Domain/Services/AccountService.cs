using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Alpha.Web.API.Security.Helpers
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IValidator<UserModel> _userModelValidator;

        public AccountService(
            SignInManager<AspNetUser> signInManager,
            IValidator<UserModel> userModelValidator)
        {
            _signInManager = signInManager;
            _userModelValidator = userModelValidator;
        }

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> ValidatePasswordAsync(UserModel userModel, string password)
        {
            _userModelValidator.ValidateAndThrow(userModel);

            var userEntity = UserModel.FillUp(userModel);

            return await _signInManager.CheckPasswordSignInAsync(userEntity, password, false);

        }
    }

}
