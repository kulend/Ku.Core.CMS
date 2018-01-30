using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Vino.Core.Infrastructure.Attributes;
using Vino.Core.Infrastructure.Extensions;

namespace Vino.Core.Infrastructure.Data
{
    public abstract class BaseSearch<T>
    {
        public Expression<Func<T, bool>> GetSearchExpression()
        {
            var parameter = Expression.Parameter(typeof(T));
            Expression<Func<T, bool>> expression = null;
            foreach (PropertyInfo p in this.GetType().GetProperties())
            {
                var exp = GetExpression(p, parameter);
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

        private Expression<Func<T, bool>> GetExpression(PropertyInfo p, ParameterExpression parameter)
        {
            var value = p.GetValue(this);
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
                        Expression.Constant(p.GetValue(this), p.PropertyType)
                    );
                    break;
                case SearchConditionOperation.NotEqual:
                    body = Expression.NotEqual(
                        Expression.PropertyOrField(parameter, name),
                        Expression.Constant(p.GetValue(this), p.PropertyType)
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
