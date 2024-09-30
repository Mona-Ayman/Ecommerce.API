
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TEntity>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges);
        Task CreateAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
