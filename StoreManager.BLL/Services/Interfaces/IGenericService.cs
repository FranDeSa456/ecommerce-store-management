using StoreManager.BLL.Models;

namespace StoreManager.BLL.Services.Interfaces
{
    public interface IGenericService<TModel>
        where TModel : class, IModelWithId
    {
        Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
