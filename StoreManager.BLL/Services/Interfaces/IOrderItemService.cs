using StoreManager.BLL.Models;

namespace StoreManager.BLL.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<OrderItemModel?> GetByIdAsync(int productId, int orderId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<OrderItemModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<OrderItemModel> AddAsync(OrderItemModel model, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(OrderItemModel model, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(int productId, int orderId, CancellationToken cancellationToken = default);
    }
}
