using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;

namespace Ku.Core.CMS.Web.Backend.Pages.MaterialCenter.Config
{
    [Auth("materialcenter.config.edit")]
    public class EditModel : BasePage
    {
        private IMaterialCenterConfigService _service;

        public EditModel(IMaterialCenterConfigService service)
        {
            _service = service;
        }

        [BindProperty]
        public MaterialCenterConfig Dto { set; get; }

        public async Task OnGetAsync()
        {
            Dto = await _service.GetAsync();
        }

        [UserOperationAttribute("修改密码")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new KuArgNullException();
            }
            await _service.SetAsync(Dto);
            return Json(true);
        }
    }
}