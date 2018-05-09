using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Ku.Core.Infrastructure.Helper
{
    public class CryptHelper
    {
        public static string EncryptMD5(string value)
        {
            var str = "";
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                str = sBuilder.ToString();
            }
            return str.ToLower();
        }

        public static string EncryptMD5Short(string value)
        {
            return EncryptMD5(value).Substring(8, 16);
        }

        public static string EncryptMD5(FileStream fs)
        {
            var str = "";
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(fs);
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                str = sBuilder.ToString();
            }
            return str.ToLower();
        }

        /// <returns>64位字符串</returns>
        public static string EncryptSha256(string orgValue)
        {
            return EncryptSha256(orgValue, Encoding.UTF8);
        }

        /// <returns>64位字符串</returns>
        public static string EncryptSha256(string orgValue, Encoding encode)
        {
            var sha256 = new HMACSHA256(Encoding.UTF8.GetBytes(orgValue));
            var bytResult = sha256.ComputeHash(Encoding.UTF8.GetBytes(orgValue));
            var result = "";
            //字节类型的数组转换为字符串 
            for (int i = 0; i < bytResult.Length; i++)
            {
                //16进制转换 
                result += string.Format("{0:x}", bytResult[i]).PadLeft(2, '0');
            }

            return result;
        }
    }
}
