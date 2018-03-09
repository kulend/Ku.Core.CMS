using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.Infrastructure.Helper;

namespace Vino.Core.Infrastructure.Extensions
{
    public static class StringExtension
    {
        public static string[] SplitRemoveEmpty(this string obj, char split)
        {
            if (obj == null)
            {
                return new string[0];
            }
            return obj.Split(new char[1] { split }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<string> SplitRemoveEmptyToList(this string obj, char split)
        {
            string[] sp;
            if (obj == null)
            {
                sp = new string[0];
            }
            sp = obj.Split(new char[1] { split }, StringSplitOptions.RemoveEmptyEntries);
            return sp.ToList();
        }

        public static long[] SplitToInt64(this string obj, char split = ',')
        {
            if (obj == null)
            {
                return new long[0];
            }
            return obj.Split(new char[1] { split }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(Int64.Parse)
                        .ToArray();
        }

        public static int[] SplitToInt32(this string obj, char split = ',')
        {
            if (obj == null)
            {
                return new int[0];
            }
            return obj.Split(new char[1] { split }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(Int32.Parse)
                        .ToArray();
        }

        public static bool EqualOrdinalIgnoreCase(this string obj, string value)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// 后缀加入指定的值
        /// </summary>
        public static string EndSuffix(this string obj, string value)
        {
            if (obj == null)
            {
                return null;
            }
            return obj.EndsWith(value) ? obj : string.Concat(obj, value);
        }

        public static string Contact(this string obj, string value)
        {
            var v = obj;
            if (obj == null)
            {
                v = "";
            }
            return string.Concat(v, value ?? "");
        }

        public static int ToInt(this string obj)
        {
            return int.Parse(obj);
        }

        public static long ToInt64(this string obj)
        {
            return long.Parse(obj);
        }

        public static decimal ToDecimal(this string obj)
        {
            return decimal.Parse(obj);
        }

        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        public static bool IsNotNullOrEmpty(this string self)
        {
            return !string.IsNullOrEmpty(self);
        }

        public static bool IsMobile(this string self)
        {
            return StringHelper.IsMobile(self);
        }

        public static string SubstringByByte(this string s, int length)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(s);
            int n = 0;  //  表示当前的字节数
            int i = 0;  //  要截取的字节数
            for (; i < bytes.GetLength(0) && n < length; i++)
            {
                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                if (i % 2 == 0)
                {
                    n++;      //  在UCS2第一个字节时n加1
                }
                else
                {
                    //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                    if (bytes[i] > 0)
                    {
                        n++;
                    }
                }
            }

            //  如果i为奇数时，处理成偶数
            if (i % 2 == 1)
            {
                //  该UCS2字符是汉字时，去掉这个截一半的汉字
                if (bytes[i] > 0)
                    i = i - 1;
                //  该UCS2字符是字母或数字，则保留该字符
                else
                    i = i + 1;
            }
            return System.Text.Encoding.Unicode.GetString(bytes, 0, i);
        }

        /// <summary>
        /// 截断字符串
        /// 如果指定的长度大于了字符串本身的长度，则自动截断
        /// <example>
        /// SubStr(0,12);
        /// </example>
        /// </summary>
        public static string Substr(this string obj, int start, int length)
        {
            if (string.IsNullOrWhiteSpace(obj))
            {
                return obj;
            }
            if ((start + length) > obj.Length)
            {
                return obj.Substring(start, obj.Length);
            }
            return obj.Substring(start, length);
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <returns></returns>
        public static string R(this string self, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(self))
            {
                return self;
            }

            return self.Replace(oldValue, newValue);
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <returns></returns>
        public static bool Eq(this string self, string value)
        {
            if (self == null) return value == null;
            return self.Equals(value);
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <returns></returns>
        public static bool NotEq(this string self, string value)
        {
            if (self == null) return value != null;
            return !self.Equals(value);
        }
    }
}
