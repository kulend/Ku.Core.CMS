using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vino.Core.CMS.Web.Base;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Pages.Account
{
    public class AccessDeniedModel : BasePage
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            throw new VinoAccessDeniedException();
        }
    }
}