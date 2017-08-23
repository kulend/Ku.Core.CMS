using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.Infrastructure.Extensions;

namespace Vino.Core.CMS.Web.Controls
{
    [HtmlTargetElement("action")]
    public class ActionHelper : TagHelper
    {
        private IFunctionService service;
        public ActionHelper(IFunctionService _service)
        {
            this.service = _service;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public string Type { set; get; } = "button";
        public string Text { set; get; } = "";
        public string Size { set; get; } = "";
        public string Theme { set; get; } = "";
        public bool Radius { set; get; } = false;
        public string Icon { set; get; }
        public string AuthCode { set; get; }

        public override async Task ProcessAsync(TagHelperContext context,TagHelperOutput output)
        {
            //检查权限
            //if (AuthCode.IsNotNullOrEmpty())
            //{
            //    var userId = ViewContext.HttpContext.User.GetUserIdOrZero();
            //    if (userId == 0)
            //    {
            //        return;
            //    }
            //    var result = await service.CheckUserAuth(userId, AuthCode);
            //    if (!result)
            //    {
            //        return;
            //    }
            //}

            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            var cls = "layui-btn";
            if (!Size.IsNullOrEmpty())
            {
                cls += $" layui-btn-{Size}";
            }
            if (!Theme.IsNullOrEmpty())
            {
                cls += $" layui-btn-{Theme}";
            }
            if (Radius)
            {
                cls += $" layui-btn-radius";
            }
            output.Attributes.SetAttribute("class", cls);
            output.Attributes.SetAttribute("type", Type);
            output.Attributes.SetAttribute("title", Text);
            var content = "";
            if (!Icon.IsNullOrEmpty())
            {
                content = $"<i class=\"layui-icon\">{Icon}</i>";
            }
            content += Text;
            output.Content.SetHtmlContent(content);
        }
    }
}
