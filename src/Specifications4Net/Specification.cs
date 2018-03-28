using System;
using System.Linq.Expressions;

namespace Specifications4Net
{
    public class Specification<T> : AbstractSpecification<T>
    {
        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }
        
        public override Expression<Func<T, bool>> Expression { get; }

    }
}
