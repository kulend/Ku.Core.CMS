using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.EventBus
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventAttribute : Attribute
    {
        public string Name { get; set; }

        public EventAttribute()
        {
        }

        public EventAttribute(string name)
        {
            Name = name;
        }
    }
}
