using AutoMapper;
using StoreManager.BLL.Models;
using StoreManager.BLL.Services.Interfaces;
using StoreManager.DAL.Repositories.Interfaces;

namespace StoreManager.BLL.Services
{
    internal class GenericService<TEntity, TModel>(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository, IMapper mapper) : IGenericService<TModel>
        where TEntity : class
        where TModel : class, IModelWithId
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository = repository;
        private readonly IMapper _mapper = mapper;
    }
}
