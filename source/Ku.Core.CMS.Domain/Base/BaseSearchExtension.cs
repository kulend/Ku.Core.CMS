using System;
using System.Linq.Expressions;
using System.Reflection;
using Ku.Core.Infrastructure.Extensions;

namespace Ku.Core.CMS.Domain
{
    public static class BaseSearchExtension
    {
        public static Expression<Func<T, bool>> GetExpression<T>(this BaseSearch<T> self)
        {
            if (self == null)
            {
                return null;
            }

            var parameter = Expression.Parameter(typeof(T));
            Expression<Func<T, bool>> expression = null;
            foreach (PropertyInfo p in self.GetType().GetProperties())
            {
                var exp = GetPropertyExpression(self, p, parameter);
                if (expression == null)
                {
                    expression = exp;
                }
                else if(exp != null)
                {
                    expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, exp.Body), parameter);
                }
            }
            return expression;
        }

        private static Expression<Func<T, bool>> GetPropertyExpression<T>(BaseSearch<T> self, PropertyInfo p, ParameterExpression parameter)
        {
            //取得SearchConditionAttribute特性
            var attr = p.GetCustomAttribute<SearchConditionAttribute>();
            var ignoreWhenNull = attr != null ? attr.IgnoreWhenNull : true;
            var value = p.GetValue(self);
            if (value == null && ignoreWhenNull)
            {
                return null;
            }
            if (p.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value) && ignoreWhenNull)
            {
                return null;
            }

            //如果有CustomConditionAttribute，则跳过该条件
            if (p.GetCustomAttribute<CustomConditionAttribute>() != null)
            {
                return null;
            }

            var name = attr != null ? (attr.PropertyName.IsNotNullOrEmpty() ? attr.PropertyName : p.Name) : p.Name;
            //根据名称取得属性
            var property = typeof(T).GetProperty(name);
            if (property == null)
            {
                return null;
            }
            var propertyType = property.PropertyType;
            ////如果是Nullable类型，只需要取得真实类。
            //if (value != null && propertyType.IsNullableType())
            //{
            //    propertyType = propertyType.RealType();
            //}

            SearchConditionOperation operation = attr != null ? attr.Operation : SearchConditionOperation.Equal;
            Expression body = null;
            switch (operation)
            {
                case SearchConditionOperation.Equal:
                    body = Expression.Equal(
                        Expression.PropertyOrField(parameter, name),
                        Expression.Constant(value, propertyType)
                    );
                    break;
                case SearchConditionOperation.NotEqual:
                    body = Expression.NotEqual(
                        Expression.PropertyOrField(parameter, name),
                        Expression.Constant(p.GetValue(self), propertyType)
                    );
                    break;
                case SearchConditionOperation.Contains:
                    body = Expression.Call(Expression.PropertyOrField(parameter, name),
                                            typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                            Expression.Constant(value, propertyType));
                    break;
                case SearchConditionOperation.NotContains:
                    body = Expression.Not(Expression.Call(Expression.PropertyOrField(parameter, name),
                                            typeof(string).GetMethod("Contains", new[] { typeof(string) }),
                                            Expression.Constant(value, propertyType)));
                    break;
                default:
                    return null;
            }

            var exp = Expression.Lambda<Func<T, bool>>(body, parameter);
            return exp;
        }
    }
}
