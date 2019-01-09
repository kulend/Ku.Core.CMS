using Dnc.Extensions.Dapper;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NLog;
using Quartz;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Job
{
    [DisallowConcurrentExecution]
    public class PageViewRecordJob : BaseJob
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public override async Task Run(IJobExecutionContext context)
        {
            logger.Info("开始处理页面访问记录...");

            IHttpClientFactory clientFactory = _provider.GetService<IHttpClientFactory>();
            
            //取得待发送列表
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<PageViewRecord>()
                    .From<PageViewRecord>()
                    .Where(new ConditionBuilder().IsNotNullOrEmpty<PageViewRecord>(m => m.ClientIp)
                            .And().IsNullOrEmpty<PageViewRecord>(m => m.Location))
                    ;

                var items = await dapper.QueryListAsync<PageViewRecord>(builder);

                foreach (var item in items)
                {
                    if ("127.0.0.1".Equals(item.ClientIp) || "::1".Equals(item.ClientIp))
                    {
                        await dapper.UpdateAsync<PageViewRecord>(new { Location = "本地" }, new { item.Id });
                    }
                    else
                    {
                        try
                        {
                            var client = clientFactory.CreateClient();
                            client.BaseAddress = new Uri("http://ip.taobao.com/");
                            var result = await client.GetAsync($"/service/getIpInfo.php?ip={item.ClientIp}");
                            var jsonStr = await result.Content.ReadAsStringAsync();
                            var json = JsonConvert.DeserializeObject<dynamic>(jsonStr);
                            //{"code":0,"data":{"ip":"183.157.11.168","country":"中国","area":"","region":"浙江","city":"杭州","county":"XX","isp":"电信","country_id":"CN","area_id":"","region_id":"330000","city_id":"330100","county_id":"xx","isp_id":"100017"}}
                            var location = $"{json.data.country}{json.data.area}{json.data.region}{json.data.city} {json.data.isp}";
                            await dapper.UpdateAsync<PageViewRecord>(new { Location = location }, new { item.Id });
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                        }

                    }
                }
            }

            logger.Info("结束处理页面访问记录...");
        }

    }
}
