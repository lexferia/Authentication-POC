using Authentication.POC.API.JWT.Data.Entities;

namespace Authentication.POC.API.JWT.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IQueryable<TEntity>> GetItems();

        Task<TEntity?> GetItem(Guid id);

        Task AddItem(TEntity item);

        Task UpdateItem(TEntity item);

        Task DeleteItem(Guid id);
    }
}
