using StoreManager.DAL.Entities;

namespace StoreManager.DAL.Repositories.Interfaces
{
    internal interface IOrderItemRepository
    {
        Task<OrderItem?> GetByIdAsync(int productId, int orderId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<OrderItem?>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(OrderItem entity, CancellationToken cancellationToken = default);
        void Update(OrderItem entity);
        void Remove(OrderItem entity);
    }
}
