using Dnc.Extensions.Ui.Layui;
using Ku.Core.CMS.IService.UserCenter;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

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
