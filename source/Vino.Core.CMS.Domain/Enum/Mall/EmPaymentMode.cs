using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vino.Core.CMS.Domain.Enum.Mall
{
    public enum EmPaymentMode : short
    {

        [Display(Name = "支付宝")]
        Alipay = 1,

        [Display(Name = "微信")]
        Weixin = 2,
    }

    public static class PaymentModeHelper {

        private static IDictionary<EmPaymentMode, PaymentModeParameter[]> items;

        static PaymentModeHelper()
        {
            items = new Dictionary<EmPaymentMode, PaymentModeParameter[]> {
                {
                    EmPaymentMode.Alipay,
                    new PaymentModeParameter[]{
                        new PaymentModeParameter{
                            Key="PartnerID",
                            Name="合作者身份",
                            Value=""
                        },
                        new PaymentModeParameter{
                            Key="Key",
                            Name="交易安全检验码",
                            Value=""
                        },
                        new PaymentModeParameter{
                            Key="Account",
                            Name="卖家支付宝账号",
                            Value=""
                        },
                        new PaymentModeParameter{
                            Key="ReturnURL",
                            Name="返回地址",
                            Value=""
                        },
                        new PaymentModeParameter{
                            Key="NotifyUrl",
                            Name="通知地址",
                            Value=""
                        }
                    }
                },
                {
                    EmPaymentMode.Weixin,
                    new PaymentModeParameter[]{
                        new PaymentModeParameter{
                            Key="AppId",
                            Name="应用ID",
                            Value=""
                        },new PaymentModeParameter{
                            Key="MchId",
                            Name="商户号",
                            Value=""
                        },new PaymentModeParameter{
                            Key="MchKey",
                            Name="商户密钥",
                            Value=""
                        },new PaymentModeParameter{
                            Key="NotifyUrl",
                            Name="通知地址",
                            Value=""
                        },new PaymentModeParameter{
                            Key="CertificatePath",
                            Name="证书路径",
                            Value=""
                        },new PaymentModeParameter{
                            Key="CertificatePwd",
                            Name="证书密码",
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

        public static PaymentModeParameter[] GetItemParameters (EmPaymentMode paymentMode)
        {
            return items.ContainsKey(paymentMode) ? items[paymentMode] : new PaymentModeParameter[]{ };
        }
    }

    public class PaymentModeParameter
    {
        public string Key { set; get; }

        public string Name { set; get; }

        public string Value { set; get; }
    }
}
