using System;
using LinqKit;

namespace Spec4Net.Criterion
{
    public class LogicalCriterion : AbstractCriterion
    {
        public ICriterion Left { get; }

        public ICriterion Right { get; }

        public PredicateOperator Op { get; }

        public LogicalCriterion(ICriterion left, ICriterion right, PredicateOperator op)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right;
            Op = op;
        }

        public override ISpecification<T> ToSpecification<T>()
        {
            return Left.ToSpecification<T>().Combine(Right.ToSpecification<T>(), Op);
        }
    }
}
