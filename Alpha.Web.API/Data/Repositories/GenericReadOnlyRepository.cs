using Alpha.Web.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public class GenericReadOnlyRepository<Id, Entity> : IGenericReadOnlyRepository<Id, Entity> where Entity : class, IEntity<Id>
    {
        private readonly AlphaDbContext _context;

        public GenericReadOnlyRepository(AlphaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().AsNoTracking().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(Id id)
        {
            //return await _context.Set<Entity>().AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

            return await _context.FindAsync<Entity>(id);
        }
    }
}
