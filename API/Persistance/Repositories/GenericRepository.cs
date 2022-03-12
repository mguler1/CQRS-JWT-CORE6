using API.Core.Application.Interfaces;
using API.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace API.Persistance.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        private readonly CoreContext _coreContext;

        public GenericRepository(CoreContext coreContext)
        {
            _coreContext = coreContext;
        }

        public async Task CreateAsync(T entity)
        {
            await _coreContext.Set<T>().AddAsync(entity);
            await _coreContext.SaveChangesAsync();
        }

        public async  Task<List<T>> GetAllAsync()
        {
            return await _coreContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async  Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _coreContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async  Task<T?> GetByIdAsync(object id)
        {
            return await _coreContext.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
             _coreContext.Set<T>().Remove(entity);
            await _coreContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _coreContext.Set<T>().Update(entity);
            await _coreContext.SaveChangesAsync();
        }
    }
}
