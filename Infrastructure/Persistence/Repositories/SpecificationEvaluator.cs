
namespace Persistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> inputQuery,            //  context.set<entity>     دي اللي هي هتعملي الجزء ده
            Specifications<T> specifications
            ) where T : class
        {
            var query = inputQuery;
            if (specifications.Cretrria is not null)
                query = query.Where(specifications.Cretrria);

            //foreach (var item in specifications.IncludeExpressions)    //  ممكن استخدم الاجريجت  بدل الجزء ده يعني استخدم اي طريقة منهم
            //    query = query.Include(item);

            query = specifications.IncludeExpressions.Aggregate(
                query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            if (specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);
            else if (specifications.OrderByDesc is not null)
                query = query.OrderByDescending(specifications.OrderByDesc);

            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            return query;
        }
    }
}
