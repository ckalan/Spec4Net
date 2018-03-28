using System;
using System.Linq.Expressions;
using LinqKit;

namespace Specifications4Net
{
    public abstract class AbstractSpecification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T,bool>> Expression { get; }

        public Predicate<T> Predicate => (target) => Expression.Invoke(target);
        
    }
}
