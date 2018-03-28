using System;
using System.Linq.Expressions;
using LinqKit;

namespace Specifications4Net
{
    public static class SpecificationExtensions
    {
        

        public static ISpecification<T> And<T>(this ISpecification<T> spec, ISpecification<T> target)
        {
            return Combine(spec, target, PredicateOperator.And);
        }

        public static ISpecification<T> And<T>(this ISpecification<T> spec, Expression<Func<T, bool>> target)
        {
            return And(spec, new Specification<T>(target));
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> spec, ISpecification<T> target)
        {
            return Combine(spec, target, PredicateOperator.Or);
        }

        public static ISpecification<T> Or<T>(this ISpecification<T> spec, Expression<Func<T, bool>> target)
        {
            return Or(spec, new Specification<T>(target));
        }

        public static ISpecification<T> Not<T>(this ISpecification<T> spec)
        {
            return new NotSpecification<T>(spec);
        }

        internal static ISpecification<T> Combine<T>(this ISpecification<T> spec, ISpecification<T> target,
            PredicateOperator op)
        {
            return new LogicalSpecification<T>(spec, target, op);
        }
    }
}
