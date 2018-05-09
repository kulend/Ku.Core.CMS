using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ku.Core.CMS.Domain.Enum.System
{
    public enum EmSmsAccountType : short
    {
        [Display(Name = "阿里云")]
        Aliyun = 1
    }

    public static class SmsAccountTypeHelper
    {

        private static IDictionary<EmSmsAccountType, SmsAccountTypeParameter[]> items;

        static SmsAccountTypeHelper()
        {
            items = new Dictionary<EmSmsAccountType, SmsAccountTypeParameter[]> {
                {
                    EmSmsAccountType.Aliyun,
                    new SmsAccountTypeParameter[]{
                        new SmsAccountTypeParameter{
                            Key="AccessKeyId",
                            Name="AccessKeyId",
                            Value=""
                        },
                        new SmsAccountTypeParameter{
                            Key="AccessKeySecret",
                            Name="AccessKeySecret",
                            Value=""
                        }
                    }
                }
            };
        }

        public static string GetItems()
        {
            return JsonConvert.SerializeObject(items);
        }

        public static SmsAccountTypeParameter[] GetItemParameters(EmSmsAccountType paymentMode)
        {
            return items.ContainsKey(paymentMode) ? items[paymentMode] : new SmsAccountTypeParameter[] { };
        }
    }

    public class SmsAccountTypeParameter
    {
        public string Key { set; get; }

        public string Name { set; get; }

        public string Value { set; get; }
    }
}
