using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.TimedTask.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SingleTaskAttribute : System.Attribute
    {
        /// <summary>
        /// 是否能同时运行
        /// </summary>
        public bool IsSingleTask { set; get; }

        public SingleTaskAttribute():this(true)
        {
        }

        public SingleTaskAttribute(bool isSingleTask)
        {
            this.IsSingleTask = isSingleTask;
        }
    }
}
