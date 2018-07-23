using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ku.Core.CMS.IService.System;
using Quartz;

namespace Ku.Core.CMS.Job
{
    public class TestJob : BaseJob
    {
        public override async Task Run(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("Greetings from HelloJob!");
        }
    }
}
