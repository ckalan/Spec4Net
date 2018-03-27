using System;
using System.Linq.Expressions;

namespace Spec4Net
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>> Expression { get; }

        Predicate<T> Predicate { get; }
    }
}
