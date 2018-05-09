using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.Infrastructure.Extensions
{
    public static class ByteExtension
    {
        public static string Format(this byte[] obj, string format)
        {
            if (obj == null)
            {
                return "";
            }

            var sBuilder = new StringBuilder();
            for (int i = 0; i < obj.Length; i++)
            {
                sBuilder.Append(obj[i].ToString(format));
            }
            return sBuilder.ToString();
        }
    }
}
