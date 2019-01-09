using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        public static string ToyyyyMM(this DateTime? obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.Value.ToString("yyyyMM");
        }

        public static string ToyyyyMM(this DateTime obj)
        {
            return obj.ToString("yyyyMM");
        }

        public static string Format(this DateTime self, string format = "")
        {
            if (self == null)
            {
                return "";
            }
            return self.ToString(format);
        }

        public static string Format(this DateTime? self, string format = "")
        {
            if (self == null)
            {
                return "";
            }
            return self.Value.ToString(format);
        }
    }
}
