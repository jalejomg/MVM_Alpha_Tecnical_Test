using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class implement logic to transact data from Users table
    /// </summary>
    public class UsersRepository : GenericRepository<string, AspNetUser>, IUsersRepository
    {
        public UsersRepository(AlphaDbContext context) : base(context)
        {
        }
    }
}
