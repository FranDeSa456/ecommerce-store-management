using AutoMapper;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;
using StoreManager.DAL.Repositories.Interfaces;

namespace StoreManager.BLL.Services
{
    public class GenericService<TEntity, TModel>(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository, IMapper mapper) : IGenericService<TModel>
        where TEntity : class
        where TModel : class, IModelWithId
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGenericRepository<TEntity> _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task<TModel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity is null ? default : _mapper.Map<TModel>(entity);
        }

        public async Task<IReadOnlyList<TModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IReadOnlyList<TModel>>(entities);
        }

        public async Task<TModel> AddAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TEntity>(model);
            await _repository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TModel>(entity);
        }

        public async Task<bool> UpdateAsync(TModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<TEntity>(model);
            _repository.Update(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            if (entity is null)
                return false;

            _repository.Remove(entity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
    }
}
