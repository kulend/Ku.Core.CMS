using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Extensions.Dapper.SqlDialect;
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

        public static DapperSql ParseToDapperSql<T>(this BaseSearch<T> search, ISqlDialect dialect)
        {
            if (search == null)
            {
                return null;
            }

            List<string> sqls = new List<string>();
            Dictionary<string, object> allParameters = new Dictionary<string, object>();
            foreach (PropertyInfo p in search.GetType().GetProperties())
            {
                (string sql, Dictionary<string, object> parameters) = ParseProperty(search, p, dialect);
                if (sql != null)
                {
                    sqls.Add(sql);
                    allParameters.TryConcat(parameters);
                }
            }

            var allSql = string.Join(" AND ", sqls.ToArray());

            return new DapperSql(allSql, allParameters);
        }

        private static (string sql, Dictionary<string, object> parameters) ParseProperty<T>(BaseSearch<T> search, PropertyInfo p, ISqlDialect dialect)
        {
            //如果有CustomConditionAttribute，则跳过该条件
            if (p.GetCustomAttribute<CustomConditionAttribute>() != null)
            {
                return (null, null);
            }

            //取得SearchConditionAttribute特性
            var attr = p.GetCustomAttribute<SearchConditionAttribute>();
            var ignoreWhenNull = attr != null ? attr.IgnoreWhenNull : true;
            var value = p.GetValue(search);

            //值为NULL的处理
            if (value == null && ignoreWhenNull)
            {
                return (null, null);
            }
            if (p.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value) && ignoreWhenNull)
            {
                return (null, null);
            }

            //取得字段名
            var name = (attr != null && attr.PropertyName.IsNotNullOrEmpty()) ? attr.PropertyName : p.Name;

            SearchConditionOperation operation = attr != null ? attr.Operation : SearchConditionOperation.Equal;
            string sql = null;
            Dictionary<string, object> pms = new Dictionary<string, object>();
            switch (operation)
            {
                case SearchConditionOperation.Equal:
                    sql = dialect.QuoteFiled(name) + "=@" + name;
                    pms.TryAdd(name, value);
                    break;
                case SearchConditionOperation.NotEqual:
                    sql = dialect.QuoteFiled(name) + "<>@" + name;
                    pms.TryAdd(name, value);
                    break;
                case SearchConditionOperation.Contains:
                    sql = $"{dialect.QuoteFiled(name)} LIKE {dialect.Concat}('{"%"}', @{name}, '{"%"}')";
                    pms.TryAdd(name, value);
                    break;
                case SearchConditionOperation.NotContains:
                    sql = $"{dialect.QuoteFiled(name)} NOT LIKE {dialect.Concat}('{"%"}', @{name}, '{"%"}')";
                    pms.TryAdd(name, value);
                    break;
                default:
                    break;
            }

            return (sql, pms);
        }
    }
}
