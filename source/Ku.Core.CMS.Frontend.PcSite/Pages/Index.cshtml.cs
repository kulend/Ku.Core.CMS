using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Web.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ku.Core.CMS.Frontend.PcSite.Pages
{
    public class IndexModel : PageModel
    {
        [PageViewRecord(PageName = "首页")]
        public void OnGet()
        {

        }
    }
}
