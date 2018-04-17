using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IgnorePageLockAttribute : Attribute
    {

    }
}
