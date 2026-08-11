namespace StoreManager.DAL.Repositories.Interfaces
{
    internal interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
