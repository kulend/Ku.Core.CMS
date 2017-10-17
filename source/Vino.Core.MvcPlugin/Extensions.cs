using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vino.Core.MvcPlugin
{
    public static class Extensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> source, Action<T> fun)
        {
            foreach (T item in source)
            {
                fun(item);
            }
            return source;
        }

        public static string ToFilePath(this string path)
        {
            return Path.Combine(path.Split('/', '\\'));
        }
    }

}
