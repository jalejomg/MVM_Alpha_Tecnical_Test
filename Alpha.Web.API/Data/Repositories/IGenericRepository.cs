using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();

        Task<Entity> GetByIdAsync(int id);

        Task<Entity> CreateAsync(Entity entity);

        Task<Entity> UpdateAsync(Entity entity);
    }
}
