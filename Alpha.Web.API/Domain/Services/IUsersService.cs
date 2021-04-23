using Alpha.Web.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public interface IUsersService
    {
        Task<UserModel> GetByIdAsync(int userId);
        Task<ResponseModel<IEnumerable<UserModel>>> ListAsync();
        Task<int> CreateAsync(UserModel userModel);
        Task<int> UpdateAsync(int userId, UserModel userModel);
        Task DeleteAsync(int userId);
    }
}
