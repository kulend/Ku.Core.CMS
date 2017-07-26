using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Core.Helper;

namespace Vino.Core.CMS.Core.Extensions
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
    }
}
