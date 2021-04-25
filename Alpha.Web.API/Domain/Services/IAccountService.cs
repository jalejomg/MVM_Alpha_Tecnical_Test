using Alpha.Web.API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Alpha.Web.API.Security.Helpers
{
    public interface IAccountService
    {
        Task<SignInResult> LoginAsync(LoginModel model);
        Task LogoutAsync();
    }
}
