using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Specifications4Net
{
    public static class ReflectionUtils
    {
        public static (PropertyInfo, Type, Expression) GetNestedPropertyExpression(
            ParameterExpression parameterExpression, Type type, string property)
        {
            string[] props = property.Split('.');
            ParameterExpression parameterExp = parameterExpression;
            Expression propertyExp = parameterExp;
            PropertyInfo propertyInfo = null;
            foreach (string prop in props)
            {
                propertyInfo = type.GetProperty(prop);
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException(
                        $"Type '{type}' does not have a public property named '{prop}' ");
                }

                propertyExp = Expression.Property(propertyExp, propertyInfo);
                type = propertyInfo.PropertyType;
            }

            return (propertyInfo, type, propertyExp);
        }
    }
}
