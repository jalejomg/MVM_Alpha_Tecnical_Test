using Alpha.Web.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Data.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class, IEntity
    {
        private readonly AlphaDbContext _context;

        public GenericRepository(AlphaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await _context.Set<Entity>().AsNoTracking().ToListAsync();
        }

        public async Task<Entity> GetByIdAsync(int id)
        {
            return await _context.Set<Entity>().AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);
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
