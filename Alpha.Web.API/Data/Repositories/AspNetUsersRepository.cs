using Alpha.Web.API.Data.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class implement logic to transact data from Users table
    /// </summary>
    public class AspNetUsersRepository : GenericRepository<string, AspNetUser>, IAspNetUsersRepository
    {

        public AspNetUsersRepository(AlphaDbContext context) : base(context)
        {
        }
    }
}
