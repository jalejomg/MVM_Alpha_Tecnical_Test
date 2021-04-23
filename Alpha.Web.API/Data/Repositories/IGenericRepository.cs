using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public interface IGenericRepository<in Id, Entity> : IGenericReadOnlyRepository<Id, Entity> where Entity : class
    {
        Task<Entity> CreateAsync(Entity entity);

        Task<Entity> UpdateAsync(Entity entity);
    }
}
