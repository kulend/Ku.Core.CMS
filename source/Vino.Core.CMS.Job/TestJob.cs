using System;
using System.Diagnostics;
using System.Threading;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Service.System;
using Vino.Core.TimedTask.Attribute;

namespace Vino.Core.CMS.Job
{
    public class TestJob: VinoJob
    {
        [Invoke(Interval = 5000)]
        [SingleTask]
        public void Run()
        {
        }

        [SingleTask]
        public void RunXXXX(IUserService service)
        {
            var aa = service.GetListAsync(0, 10);
            Debug.WriteLine(DateTime.Now + " Test RunXXXX invoke...");
        }
    }
}
