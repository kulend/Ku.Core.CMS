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
        public static List<(string value, string key, string name)> GetEnumInfos(this Type obj)
        {
            List<(string, string, string)> list = new List<(string, string, string)>();
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

            foreach (var i in Enum.GetValues(obj))
            {
                var name = Enum.GetName(obj, i);
                list.Add((((short)i).ToString(), name, dict.ContainsKey(name) ? dict[name] : name));
            }
            return list;
        }

        public static string GetEnumDisplayName(this object obj)
        {
            if (!obj.GetType().IsEnum)
            {
                return "";
            }

            var typeInfo = obj.GetType().GetTypeInfo();
            MemberInfo memberInfo = typeInfo.GetMember(obj.ToString()).FirstOrDefault();
            if (memberInfo == null)
            {
                return "";
            }
            var attr = memberInfo.GetCustomAttribute<DisplayAttribute>();
            if (attr == null)
            {
                return memberInfo.Name;
            }

            return attr.Name;
        }
    }
}
