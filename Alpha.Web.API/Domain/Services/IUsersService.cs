using Alpha.Web.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public interface IUsersService
    {
        Task<UserModel> GetByIdAsync(string userId);
        Task<ResponseModel<IEnumerable<UserModel>>> ListAsync();
        Task<string> CreateAsync(UserModel userModel);
        Task<string> UpdateAsync(string userId, UserModel userModel);
        Task DeleteAsync(string userId);
    }
}
