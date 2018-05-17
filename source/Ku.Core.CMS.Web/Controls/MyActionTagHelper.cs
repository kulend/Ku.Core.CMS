using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.Extensions.Ui;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.CMS.IService.UserCenter;

namespace Ku.Core.CMS.Web.Controls
{
    [HtmlTargetElement("action")]
    public class MyActionTagHelper : ActionTagHelper
    {
        private IBackendAuthService _service;

        public MyActionTagHelper(IBackendAuthService service, IActionTagProcess process)
            : base(process)
        {
            this._service = service;
        }

        public string AuthCode { set; get; }

        public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output)
        {
            //检查权限
            if (AuthCode.IsNotNullOrEmpty())
            {
                var userId = ViewContext.HttpContext.User.GetUserIdOrZero();
                if (userId == 0)
                {
                    return;
                }
                var result = await _service.CheckUserAuthAsync(userId, AuthCode);
                if (!result)
                {
                    return;
                }
            }

            await base.ProcessAsync(context, output);
        }
    }
}
