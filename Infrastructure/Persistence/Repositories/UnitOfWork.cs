using System.Collections.Concurrent;

namespace Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork

    {
        public readonly StoreContext _context;
        //public readonly Dictionary<string, object> _repos;
        public readonly ConcurrentDictionary<string, object> _repos;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
            //_repos = new ConcurrentDictionary<string,object>();
            _repos = new();    //ممكن اكتبها كده عادي
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>   //  دي قانكشن هتكريت اوبجكت من الريبو اللي عاوزاها لو مش موجود لكن لو موجود هترجعه 
          => (IGenericRepository<TEntity, TKey>)_repos.GetOrAdd(typeof(TEntity).Name, _ => new GenericRepository<TEntity, TKey>(_context));
        //{   دي طريقة لو هستخدم الديكشناري لكن انا هستخدم كونكرنت ديكشناري فعمل الطريقة اللي فوق دي
        //    var typeName = typeof(TEntity).Name;
        //    if (_repos.ContainsKey(typeName)) return (IGenericRepository<TEntity, TKey>)_repos[typeName];
        //    var repo = new GenericRepository<TEntity, TKey>(_context);
        //    _repos.Add(typeName, repo);
        //    return repo;
        //}
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
