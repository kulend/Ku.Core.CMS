using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// 判断是否为Nullable类型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type self)
        {
            if (self == null)
            {
                return false;
            }

            return (self.IsGenericType && self.GetGenericTypeDefinition().Equals(typeof(Nullable<>)));
        }

        /// <summary>
        /// 取得真实类型
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Type RealType(this Type self)
        {
            if (IsNullableType(self))
            {
                return self.GetGenericArguments()[0];
            }
            return self;
        }
    }
}
