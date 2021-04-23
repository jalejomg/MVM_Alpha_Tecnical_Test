using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public interface IGenericReadOnlyRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
    }
}
