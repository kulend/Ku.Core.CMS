using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.TimedTask.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InvokeAttribute : System.Attribute
    {
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// //设置是执行一次（false）还是一直执行(true)，默认为true
        /// </summary>
        public bool AutoReset { set; get; } = true;

        public int Interval { get; set; } = 1000 * 60; // 1分钟

        public DateTime BeginTime { set; get; }

        public DateTime ExpireTime { set; get; }
    }
}
