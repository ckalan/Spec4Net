using System;
using System.Linq.Expressions;
using Exp = System.Linq.Expressions.Expression;

namespace Specifications4Net
{
    public class NotSpecification<T> : AbstractSpecification<T>
    {
        public ISpecification<T> Target { get; }

        public NotSpecification(ISpecification<T> target)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public override Expression<Func<T, bool>> Expression =>
            Exp.Lambda<Func<T, bool>>(Exp.Not(Target.Expression.Body), Target.Expression.Parameters);
    }
}
