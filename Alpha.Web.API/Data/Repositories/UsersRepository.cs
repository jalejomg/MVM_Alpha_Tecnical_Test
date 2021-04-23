using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(AlphaDbContext context) : base(context)
        {
        }
    }
}
