using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications4Net.Criterion
{
    public class SimpleCriterion : AbstractCriterion
    {
        public static readonly string[] SupportedOperations = {"eq", "neq", "lt", "lte", "gt", "gte"};

        public SimpleCriterion(string propertyName, object value, string op)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName));

            PropertyName = propertyName;
            Value = value;
            Op = op ?? throw new ArgumentNullException(nameof(op));

            if (!SupportedOperations.Contains(Op))
            {
                throw new ArgumentException(
                    $"Operator '{op}' is not supported. Supported operations are: {string.Join(",", SupportedOperations)}");
            }
        }

        public SimpleCriterion(string propertyName, object value, string op, bool ignoreCase) : this(propertyName,
            value, op)
        {
            WithIgnoreCase();
        }

        public SimpleCriterion WithIgnoreCase()
        {
            IgnoreCase = true;
            return this;
        }


        public string PropertyName { get; }

        public object Value { get; }

        public string Op { get; }

        public bool IgnoreCase { get; private set; }


        public override ISpecification<T> ToSpecification<T>()
        {
            var type = typeof(T);
            ParameterExpression parameterExp = Expression.Parameter(type, "x");
            var (_, propType, propertyExp) =
                ReflectionUtils.GetNestedPropertyExpression(parameterExp,typeof(T), PropertyName);

            Expression predicate = GetPredicateExpression<T>(propType, propertyExp);
            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameterExp);

            return new Specification<T>(lambda);
        }

        protected virtual Expression GetPredicateExpression<T>(Type type, Expression expressionLeft)
        {
            var op = Op.ToLowerInvariant();
            var expressionRight = Expression.Convert(Expression.Constant(Value), type);

            switch (op)
            {
                case "eq":
                    return Expression.Equal(expressionLeft, expressionRight);
                case "neq":
                    return Expression.NotEqual(expressionLeft, expressionRight);
                case "lt":
                    return Expression.LessThan(expressionLeft, expressionRight);
                case "lte":
                    return Expression.LessThanOrEqual(expressionLeft, expressionRight);
                case "gt":
                    return Expression.GreaterThan(expressionLeft, expressionRight);
                case "gte":
                    return Expression.GreaterThanOrEqual(expressionLeft, expressionRight);
            }

            throw new NotSupportedException($"Operator {Op} is not supported");
        }
    }
}
