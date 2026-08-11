using Microsoft.EntityFrameworkCore;
using StoreManager.DAL.Data;
using StoreManager.DAL.Repositories.Interfaces;
using StoreManager.DAL.Entities;

namespace StoreManager.DAL.Repositories
{
    internal class OrderItemRepository(StoreDbContext context) : IOrderItemRepository
    {
        private readonly DbSet<OrderItem> _dbSet = context.Set<OrderItem>();

        public async Task<OrderItem?> GetByIdAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.ProductId == productId && oi.OrderId == orderId, cancellationToken);
        }

        public async Task<IReadOnlyList<OrderItem?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(oi => oi.Product)
                .Include(oi => oi.Order)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(OrderItem entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public void Update(OrderItem entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(OrderItem entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
