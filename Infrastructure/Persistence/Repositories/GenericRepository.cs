using Domain.Contracts;
using Persistence.Data;
namespace Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task CreateAsync(TEntity entity) => await _storeContext.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity) => _storeContext.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => _storeContext.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        => trackChanges ? await _storeContext.Set<TEntity>().ToListAsync() : await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id) => await _storeContext.Set<TEntity>().FindAsync(id);

        public async Task<TEntity?> GetAsync(Specifications<TEntity> specifications)
        => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
           => await ApplySpecifications(specifications).ToListAsync();

        private IQueryable<TEntity> ApplySpecifications(Specifications<TEntity> specifications)
            => SpecificationEvaluator.GetQuery<TEntity>(_storeContext.Set<TEntity>(), specifications);

        public async Task<int> CountAsync(Specifications<TEntity> specifications)
        =>await ApplySpecifications(specifications).CountAsync();
    }
}
