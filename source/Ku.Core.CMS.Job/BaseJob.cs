using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Job
{
    public abstract class BaseJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            long taskId = context.JobDetail.JobDataMap.GetLong("TaskId");
            await Run(context);
        }

        public abstract Task Run(IJobExecutionContext context);
    }
}
