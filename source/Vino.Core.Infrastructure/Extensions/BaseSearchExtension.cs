using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.Infrastructure.Extensions
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
                else
                {
                    expression = Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expression.Body, exp.Body), parameter);
                }

            }
            return expression;
        }

        private static Expression<Func<T, bool>> GetPropertyExpression<T>(BaseSearch<T> self, PropertyInfo p, ParameterExpression parameter)
        {
            var value = p.GetValue(self);
            if (value == null)
            {
                return null;
            }
            if (p.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value))
            {
                return null;
            }
            var attr = p.GetCustomAttribute<SearchConditionAttribute>();
            var name = attr != null ? (attr.PropertyName.IsNotNullOrEmpty() ? attr.PropertyName : p.Name) : p.Name;
            SearchConditionOperation operation = attr != null ? attr.Operation : SearchConditionOperation.Equal;
            BinaryExpression body = null;
            switch (operation)
            {
                case SearchConditionOperation.Equal:
                    body = Expression.Equal(
                        Expression.PropertyOrField(parameter, name),
                        Expression.Constant(p.GetValue(self), p.PropertyType)
                    );
                    break;
                case SearchConditionOperation.NotEqual:
                    body = Expression.NotEqual(
                        Expression.PropertyOrField(parameter, name),
                        Expression.Constant(p.GetValue(self), p.PropertyType)
                    );
                    break;
                case SearchConditionOperation.Contains:
                    break;
                default:
                    return null;
            }

            var exp = Expression.Lambda<Func<T, bool>>(body, parameter);
            return exp;
        }
    }
}
