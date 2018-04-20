using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        public static List<(int value, string key, string name)> GetEnuInfos(this Type obj)
        {
            List<(int, string, string)> list = new List<(int, string, string)>();
            if (obj == null)
            {
                return list;
            }
            if (!obj.IsEnum)
            {
                return list;
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            FieldInfo[] fieldinfo = obj.GetFields();
            foreach (FieldInfo item in fieldinfo)
            {
                var attr = item.GetCustomAttribute<DisplayAttribute>();
                if (attr != null)
                {
                    dict.Add(item.Name, attr.Name);
                }
            }

            foreach (int i in Enum.GetValues(obj))
            {
                var name = Enum.GetName(obj, i);
                list.Add((i, name, dict.ContainsKey(name) ? dict[name] : name));
            }
            return list;
        }
    }
}
