using StoreManager.DAL.Data;
using StoreManager.DAL.Repositories.Interfaces;

namespace StoreManager.DAL.Repositories
{
    public class UnitOfWork(StoreDbContext context) : IUnitOfWork
    {
        private readonly StoreDbContext _context = context;
        private bool _disposed;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken); 
        }

        public void Dispose() { Dispose(true); GC.SuppressFinalize(this); }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _context.Dispose();
            _disposed = true;
        }
    }
}
