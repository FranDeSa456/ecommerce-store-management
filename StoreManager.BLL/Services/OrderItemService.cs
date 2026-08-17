using AutoMapper;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;
using StoreManager.DAL.Entities;
using StoreManager.DAL.Repositories.Interfaces;

namespace StoreManager.BLL.Services
{
    public class OrderItemService(IUnitOfWork unitOfWork, IOrderItemRepository repository, IMapper mapper) : IOrderItemService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IOrderItemRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<OrderItemModel?> GetByIdAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(productId, orderId, cancellationToken);
            return entity is null ? default : _mapper.Map<OrderItemModel>(entity);
        }

        public async Task<IReadOnlyList<OrderItemModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<OrderItemModel>>(entities);
        }

        public async Task<OrderItemModel> AddAsync(OrderItemModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<OrderItem>(model);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OrderItemModel>(entity);
        }

        public async Task<bool> UpdateAsync(OrderItemModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<OrderItem>(model);
            _repository.Update(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> RemoveAsync(int productId, int orderId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(productId, orderId, cancellationToken);
            if (entity is null)
                return false;

            _repository.Remove(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
