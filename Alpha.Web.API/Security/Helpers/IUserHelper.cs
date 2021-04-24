using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Alpha.Web.API.Security.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(User user, string roleName);

        Task<bool> IsUserInRoleAsync(User user, string roleName);
        Task<SignInResult> LoginAsync(LoginModel model);
        Task LogoutAsync();

    }
}
