using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.Extensions.Ui;
using Vino.Core.Infrastructure.Extensions;

namespace Vino.Core.CMS.Web.Controls
{
    [HtmlTargetElement("action")]
    public class MyActionTagHelper : ActionTagHelper
    {
        private IFunctionService _service;

        public MyActionTagHelper(IFunctionService service, IActionTagProcess process)
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
                var result = await _service.CheckUserAuth(userId, AuthCode);
                if (!result)
                {
                    return;
                }
            }

            await base.ProcessAsync(context, output);
        }
    }
}
