using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Vino.Core.CMS.Core.Helper
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
    }
}
