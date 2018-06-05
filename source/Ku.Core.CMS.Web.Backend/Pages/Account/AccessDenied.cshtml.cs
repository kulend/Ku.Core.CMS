using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.Account
{
    public class AccessDeniedModel : BasePage
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            throw new KuAccessDeniedException();
        }
    }
}