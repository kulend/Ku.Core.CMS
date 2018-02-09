using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class EnumerableExtension
    {
        public static string SelectJoin<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, string spseparator)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return string.Join(spseparator, source.Select(selector).ToArray());
        }
    }
}
