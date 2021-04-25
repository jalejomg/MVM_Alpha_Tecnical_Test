using Alpha.Web.API.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public interface IAspNetUsersService
    {
        Task<UserModel> GetByIdAsync(string userId);
        Task<ResponseModel<IEnumerable<UserModel>>> ListAsync();
        Task<IdentityResult> AddAsync(UserModel user, string password);
        Task<string> UpdateAsync(string userId, UserModel userModel);
        Task CheckRoleAsync(int roleName);
        Task AddUserToRoleAsync(UserModel userModel, int roleCode);
        Task<bool> IsUserInRoleAsync(UserModel userModel, int roleCode);
        Task DeleteAsync(string userId);
    }
}
