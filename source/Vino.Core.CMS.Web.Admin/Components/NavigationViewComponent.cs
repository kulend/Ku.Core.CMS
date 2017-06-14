using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Vino.Core.CMS.Web.Admin.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        public NavigationViewComponent()
        {

        }

        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
