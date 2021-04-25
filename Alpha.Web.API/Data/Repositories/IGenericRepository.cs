using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This interface declare method to write data on the database
    /// </summary>
    /// <typeparam name="Id"></typeparam>
    /// <typeparam name="Entity"></typeparam>
    public interface IGenericRepository<in Id, Entity> : IGenericReadOnlyRepository<Id, Entity> where Entity : class
    {
        Task<Entity> CreateAsync(Entity entity);

        Task<Entity> UpdateAsync(Entity entity);
    }
}
