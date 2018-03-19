using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class ObjectExtension
    {
        public static string JsonSerialize(this object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return JsonConvert.SerializeObject(obj);
        }

        public static T Copy<T>(this T obj)
        {
            T d = Activator.CreateInstance<T>(); //构造新实例
            try
            {
                var Types = obj.GetType();//获得类型  
                var Typed = typeof(T);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    sp.SetValue(d, sp.GetValue(obj, null), null);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
    }
}
