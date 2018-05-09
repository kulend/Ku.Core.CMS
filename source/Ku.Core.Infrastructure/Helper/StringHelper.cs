using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ku.Core.Infrastructure.Helper
{
    public class StringHelper
    {
        public static bool IsMobile(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            //检查手机号码格式
            Regex regex = new Regex(@"^1[3|4|5|7|8|9][0-9]\d{8}$");
            return regex.IsMatch(value);
        }
    }
}
