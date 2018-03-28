using System;
using System.Linq;
using System.Linq.Expressions;

namespace Specifications4Net.Query
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, ISpecification<T> specification)
        {
            return queryable.Where(specification.Expression);
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable,
            params (string Property, string OrderType)[] orderProperties)
        {
            var result = queryable;
            for (var i = 0; i < orderProperties.Length; i++)
            {
                var orderProp = orderProperties[i];
                var methodName = GetOrderMethodName(i, orderProp.OrderType);
                result = result.ApplyOrder(orderProp.Property, methodName);
            }

            return result;
        }


        private static string GetOrderMethodName(int propertyIndex, string orderType)
        {
            var methodName = propertyIndex == 0 ? "OrderBy" : "ThenBy";
            if (!string.IsNullOrWhiteSpace(orderType))
            {
                orderType = orderType.ToLowerInvariant();
                if (orderType == "desc" || orderType == "descending")
                {
                    methodName += "Descending";
                }
            }

            return methodName;
        }

        public static IOrderedQueryable<T> ApplyOrder<T>(this IQueryable<T> source, string property, string methodName)
        {
            var type = typeof(T);
            ParameterExpression parameterExp = Expression.Parameter(type, "x");
            var (_, propType, propertyExp) =
                ReflectionUtils.GetNestedPropertyExpression(parameterExp, type, property);

            Type delegateType = typeof(Func<,>).MakeGenericType(type, propType);
            LambdaExpression lambda = Expression.Lambda(delegateType, propertyExp, parameterExp);

            object result = typeof(Queryable).GetMethods()
                .Single(method =>
                    method.Name == methodName && method.IsGenericMethodDefinition &&
                    method.GetGenericArguments().Length == 2 && method.GetParameters().Length == 2)
                .MakeGenericMethod(type, propType).Invoke(null, new object[] {source, lambda});
            return (IOrderedQueryable<T>) result;
        }
    }
}
