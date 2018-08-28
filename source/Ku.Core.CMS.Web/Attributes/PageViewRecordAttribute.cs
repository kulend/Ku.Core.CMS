using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PageViewRecordAttribute : Attribute
    {
        public string PageName { set; get; }
    }
}
