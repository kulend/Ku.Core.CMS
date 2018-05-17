using System;
using System.Diagnostics;
using Ku.Core.CMS.IService.System;
using Ku.Core.TimedTask.Attribute;

namespace Ku.Core.CMS.Job
{
    public class TestJob: VinoJob
    {
        //[Invoke(Interval = 5000)]
        [SingleTask]
        public void Run()
        {
        }
    }
}
