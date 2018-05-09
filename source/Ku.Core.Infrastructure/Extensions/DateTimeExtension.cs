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
    }
}
