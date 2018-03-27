using System;
using System.Linq.Expressions;
using LinqKit;

namespace Spec4Net
{
    public class LogicalSpecification<T> : AbstractSpecification<T>
    {
        public LogicalSpecification(ISpecification<T> left, ISpecification<T> right, PredicateOperator @operator)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
            Operator = @operator;
        }

        public ISpecification<T> Left { get; }

        public ISpecification<T> Right { get; }

        public PredicateOperator Operator { get; }

        public override Expression<Func<T, bool>> Expression => Left.Expression.Extend(Right.Expression, Operator);
    }
}
