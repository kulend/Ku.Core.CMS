using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ku.Core.CMS.Data.Repository.System;
using Ku.Core.CMS.Domain.Enum.System;
using Ku.Core.Communication.SMS;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.TimedTask.Attribute;

namespace Ku.Core.CMS.Job
{
    public class SmsJob : VinoJob
    {
        [Invoke(Interval = 15000)]
        [SingleTask]
        public async Task Run(ISmsQueueRepository queueRepository)
        {
            Debug.WriteLine(DateTime.Now + " 开始处理短信事务...");
            //取得待发送列表
            var query = queueRepository.GetQueryable();
            query = query.Include(x=>x.Sms).ThenInclude(x=>x.SmsTemplet).ThenInclude(x=>x.SmsAccount);
            query = query.Where(x => x.Status == EmSmsQueueStatus.WaitSend && !x.IsDeleted);
            var items = await query.ToListAsync();
            foreach (var item in items)
            {
                if (item.Sms == null || item.Sms.SmsTemplet == null || item.Sms.SmsTemplet.SmsAccount == null)
                {
                    continue;
                }
                using (var trans = await queueRepository.BeginTransactionAsync())
                {
                    if (item.ExpireTime < DateTime.Now)
                    {
                        //已过期
                        item.Status = EmSmsQueueStatus.Expired;
                    }
                    else
                    {
                        //发送短信
                        var account = item.Sms.SmsTemplet.SmsAccount;
                        var sms = new SmsObject
                        {
                            Mobile = item.Sms.Mobile,
                            Signature = item.Sms.SmsTemplet.Signature,
                            TempletKey = item.Sms.SmsTemplet.TempletKey,
                            Data = JsonConvert.DeserializeObject<IDictionary<string, string>>(item.Sms.Data),
                            OutId = item.Id.ToString()
                        };

                        var dict = JsonConvert.DeserializeObject<IDictionary<string, string>>(account.ParameterConfig);
                        var accessKeyId = dict["AccessKeyId"];
                        var accessKeySecret = dict["AccessKeySecret"];

                        (bool success, string response) res;
                        switch (account.Type)
                        {
                            case EmSmsAccountType.Aliyun:
                                res = await new AliyunSmsSender(accessKeyId, accessKeySecret).Send(sms);
                                break;
                            default:
                                res = (false, "短信账户功能未实现");
                                break;
                        }

                        item.Response = res.response.Substr(0, 1000);
                        item.Status = res.success ? EmSmsQueueStatus.Sent : EmSmsQueueStatus.Error;
                        item.SendTime = DateTime.Now;
                    }
                    queueRepository.Update(item);
                    await queueRepository.SaveAsync();
                    trans.Commit();
                }
            }
            Debug.WriteLine(DateTime.Now + " 结束处理短信事务...");
        }
    }
}
