using Alpha.Web.API.Data.Entities;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class declare methods to transact data from Users table
    /// </summary>
    public interface IAspNetUsersRepository : IGenericRepository<string, AspNetUser>
    {
    }
}
