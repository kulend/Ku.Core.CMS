using Ku.Core.CMS.IService.System;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Ku.Core.CMS.Domain.Enum.System;

namespace Ku.Core.CMS.Job
{
    public abstract class BaseJob : IJob
    {
        protected IServiceProvider _provider;

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _provider = (IServiceProvider)context.Scheduler.Context.Get("IServiceProvider");
                var service = _provider.GetService<ITimedTaskService>();

                long taskId = context.JobDetail.JobDataMap.GetLong("TaskId");
                //更新状态
                await service.UpdateTaskStatusAsync(taskId, new {
                    Status = EmTimedTaskStatus.Running,
                    StarRunTime = context.FireTimeUtc.DateTime,
                    EndRunTime = (Nullable<DateTime>)null,
                    NextRunTime = context.NextFireTimeUtc?.DateTime
                });

                Run(context).GetAwaiter().GetResult();

                //更新状态
                await service.UpdateTaskStatusAsync(taskId, new
                {
                    Status = EmTimedTaskStatus.Finish,
                    EndRunTime = DateTime.Now
                });

                await service.IncreaseRunTimesAsync(taskId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public abstract Task Run(IJobExecutionContext context);
    }
}
