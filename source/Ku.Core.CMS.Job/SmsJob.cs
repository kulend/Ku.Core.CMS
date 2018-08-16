using Dnc.Extensions.Dapper;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Entity.Communication;
using Ku.Core.CMS.Domain.Enum.Communication;
using Ku.Core.Communication.SMS;
using Ku.Core.Infrastructure.Extensions;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Job
{
    [DisallowConcurrentExecution]
    public class SmsJob : BaseJob
    {
        public override async Task Run(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("开始处理短信事务...");

            var count = 0;
            //取得待发送列表
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<SmsQueue>().Concat<Sms>().Concat<SmsTemplet>().Concat<SmsAccount>()
                    .From<SmsQueue>()
                    .InnerJoin<Sms>().On(new ConditionBuilder().Equal<SmsQueue, Sms>(m => m.SmsId, s => s.Id))
                    .InnerJoin<SmsTemplet>().On(new ConditionBuilder().Equal<Sms, SmsTemplet>(s => s.SmsTempletId, t => t.Id))
                    .InnerJoin<SmsAccount>().On(new ConditionBuilder().Equal<SmsTemplet, SmsAccount>(t => t.SmsAccountId, a => a.Id))
                    .Where(new ConditionBuilder().Equal<SmsQueue>(m => m.Status, EmSmsQueueStatus.WaitSend)
                            .And().Equal<SmsQueue>(m => m.IsDeleted, false)
                            .And().Equal<Sms>(s => s.IsDeleted, false)
                            .And().Equal<SmsTemplet>(t => t.IsDeleted, false)
                            .And().Equal<SmsAccount>(a => a.IsDeleted, false))
                    ;

                var items = await dapper.QueryListAsync<SmsQueue, Sms, SmsTemplet, SmsAccount, SmsQueue>(builder, (m, s, t, a) =>
                {
                    m.Sms = s;
                    s.SmsTemplet = t;
                    t.SmsAccount = a;
                    return m;
                }, splitOn: "Id");

                foreach (var item in items)
                {
                    using (var trans = dapper.BeginTrans())
                    {
                        if (item.ExpireTime < DateTime.Now)
                        {
                            //已过期
                            item.Status = EmSmsQueueStatus.Expired;
                            await dapper.UpdateAsync<SmsQueue>(new { item.Status }, new { item.Id });
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

                            await dapper.UpdateAsync<SmsQueue>(new { item.Status, item.SendTime, item.Response }, new { item.Id });

                            count++;
                        }
                        trans.Commit();
                    }
                }

            }

            await Console.Out.WriteLineAsync($"共发送{count}条短信");

            await Console.Out.WriteLineAsync("结束处理短信事务...");
        }

    }
}
