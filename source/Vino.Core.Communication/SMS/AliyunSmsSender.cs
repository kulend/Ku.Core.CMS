using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vino.Core.Communication.SMS
{
    public class AliyunSmsSender : ISmsSender
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        public async Task<string> Send(SmsObject sms, IDictionary<string, string> parms)
        {
            string product = parms["Product"] ?? "Dysmsapi";//短信API产品名称
            string domain = parms["Domain"] ?? "dysmsapi.aliyuncs.com";//短信API产品域名
            string accessKeyId = parms["AccessKeyId"];//你的accessKeyId
            string accessKeySecret = parms["AccessKeySecret"];//你的accessKeySecret

            //检查参数
            if (string.IsNullOrEmpty(product) || string.IsNullOrEmpty(domain) 
                || string.IsNullOrEmpty(accessKeyId) || string.IsNullOrEmpty(accessKeySecret))
            {
                throw new ArgumentNullException("短信账户参数未设置正确！");
            }

            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", accessKeyId, accessKeySecret);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
            request.PhoneNumbers = sms.Mobile;
            //必填:短信签名-可在短信控制台中找到
            request.SignName = sms.Signature;
            //必填:短信模板-可在短信控制台中找到
            request.TemplateCode = sms.TempletKey;
            //可选:模板中的变量替换JSON串
            request.TemplateParam = JsonConvert.SerializeObject(sms.Data);
            //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
            request.OutId = sms.OutId.ToString();
            //请求失败这里会抛ClientException异常
            SendSmsResponse sendSmsResponse = await acsClient.GetAcsResponse(request);
            if ("OK".Equals(sendSmsResponse.Code))
            {
                return $"{sendSmsResponse.Message}";
            }
            else
            {
                throw new Exception($"{sendSmsResponse.Code}:{sendSmsResponse.Message}");
            }
        }
    }
}
