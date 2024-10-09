
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {

        protected Specifications(Expression<Func<T, bool>>? cretrria)
        {
            Cretrria = cretrria;
        }

        public Expression<Func<T, bool>>? Cretrria { get; }
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDesc { get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddIncludes(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);

        protected void SetOrderBy(Expression<Func<T, object>> expression)
         => OrderBy = expression;
        protected void SetOrderByDesc(Expression<Func<T, object>> expression)
         => OrderByDesc = expression;

        protected void SetPagination(int pageIndex, int pageSize)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

    }
}
