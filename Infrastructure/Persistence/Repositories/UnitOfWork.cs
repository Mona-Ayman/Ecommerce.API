using System.Collections.Concurrent;

namespace Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork

    {
        public readonly StoreContext _context;
        public readonly ConcurrentDictionary<string, object> _repos;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            //_repos = new ConcurrentDictionary<string,object>();
            _repos = new();    //ممكن اكتبها كده عادي
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
          => (IGenericRepository<TEntity, TKey>)_repos.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(_context));

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
