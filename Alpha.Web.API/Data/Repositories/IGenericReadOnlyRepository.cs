using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This interface declare methods to get data from database
    /// </summary>
    /// <typeparam name="Id"></typeparam>
    /// <typeparam name="Entity"></typeparam>
    public interface IGenericReadOnlyRepository<in Id, Entity> where Entity : class
    {
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(Id id);
    }
}
