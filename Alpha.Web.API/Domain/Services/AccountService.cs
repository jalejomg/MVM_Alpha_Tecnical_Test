using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Alpha.Web.API.Security.Helpers
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AspNetUser> _signInManager;


        public AccountService(
            SignInManager<AspNetUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(LoginModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
