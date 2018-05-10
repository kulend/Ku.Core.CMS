using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ku.Core.Infrastructure.Extensions
{
    public static class IDictionaryExtension
    {
        public static void TryConcat<K, V>(this IDictionary<K, V> self, params IDictionary<K, V>[] others)
        {
            if (others == null || others.Length == 0)
            {
                return;
            }
            if (self == null)
            {
                self = new Dictionary<K, V>();
            }
            foreach (var other in others.Where(x=>x != null && x.Count > 0))
            {
                foreach (var item in other)
                {
                    self.TryAdd(item.Key, item.Value);
                }
            }
        }
    }
}
