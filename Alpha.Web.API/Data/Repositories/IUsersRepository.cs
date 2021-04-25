using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class declare methods to transact data from Users table
    /// </summary>
    public interface IUsersRepository : IGenericRepository<string, AspNetUser>
    {
    }
}
