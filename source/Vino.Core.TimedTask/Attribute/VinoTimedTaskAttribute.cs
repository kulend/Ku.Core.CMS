using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.TimedTask.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class VinoTimedTaskAttribute : System.Attribute
    {
        public string Name { set; get; }

        public VinoTimedTaskAttribute()
        {
        }

        public VinoTimedTaskAttribute(string name)
        {
            this.Name = name;
        }
    }
}
