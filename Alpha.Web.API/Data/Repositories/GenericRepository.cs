using Alpha.Web.API.Data.Entities;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public class GenericRepository<Id, Entity> : GenericReadOnlyRepository<Id, Entity>,
        IGenericRepository<Id, Entity> where Entity : class, IEntity<Id>
    {
        private readonly AlphaDbContext _context;

        public GenericRepository(AlphaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Entity> CreateAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<Entity> UpdateAsync(Entity entity)
        {
            _context.Set<Entity>().Update(entity);
            await SaveAllAsync();
            return entity;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
