using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UserActionAttribute : Attribute
    {
        public string Operation { set; get; }

        public UserActionAttribute()
        {
        }

        public UserActionAttribute(string operation)
        {
            this.Operation = operation;
        }

    }
}
