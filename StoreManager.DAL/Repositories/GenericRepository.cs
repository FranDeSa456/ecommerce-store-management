using Microsoft.EntityFrameworkCore;
using StoreManager.DAL.Data;
using StoreManager.DAL.Repositories.Interfaces;

namespace StoreManager.DAL.Repositories
{
    internal class GenericRepository<T>(StoreDbContext context) : IGenericRepository<T>
        where T : class 
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync([id], cancellationToken);
        }

        public async Task<IReadOnlyList<T?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
