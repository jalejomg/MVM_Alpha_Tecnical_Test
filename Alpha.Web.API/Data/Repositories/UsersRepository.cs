using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    public class UsersRepository : GenericRepository<string, User>, IUsersRepository
    {
        public UsersRepository(AlphaDbContext context) : base(context)
        {
        }
    }
}
