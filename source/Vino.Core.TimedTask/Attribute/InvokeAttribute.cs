using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.TimedTask.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InvokeAttribute : System.Attribute
    {
        public bool IsEnabled { get; set; } = true;

        public ScheduleType ScheduleType { set; get; } = ScheduleType.Cycle;

        public int Interval { get; set; } = 1000 * 60 * 60 * 24; // 24 hours

        public string RunTime { set; get; }

        public bool SkipWhileExecuting { get; set; } = false;

        public DateTime BeginTime { set; get; }

        public DateTime ExpireTime { set; get; }
    }
}
