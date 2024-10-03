
using System.Linq.Expressions;

namespace Domain.Contracts
{
    public abstract class Specifications<T> where T : class
    {
      
        protected Specifications(Expression<Func<T, bool>>? cretrria)
        {
            Cretrria = cretrria;
        }

        public Expression<Func<T,bool>>? Cretrria { get;  }


        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

        protected void AddIncludes(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);

    }
}
