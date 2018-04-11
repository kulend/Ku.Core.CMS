using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.System;
using Vino.Core.CMS.Domain.Enum.System;
using Vino.Core.Communication.SMS;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.TimedTask.Attribute;

namespace Vino.Core.CMS.Job
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
                            OutId = item.Id
                        };

                        ISmsSender sender = null;
                        string result;
                        if (account.Type == EmSmsAccountType.Aliyun) sender = new AliyunSmsSender();
                        try
                        {
                            result = await sender.Send(sms, JsonConvert.DeserializeObject<IDictionary<string, string>>(account.ParameterConfig));
                            item.Response = result.Substr(0, 1000);
                            item.Status = EmSmsQueueStatus.Sent;
                            item.SendTime = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            item.Status = EmSmsQueueStatus.Error;
                            item.SendTime = DateTime.Now;
                            item.Response = ex.Message.Substr(0, 1000);
                        }
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
