using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Vino.Core.CMS.Core.Extensions;

namespace Vino.Core.CMS.Web.Controls
{
    [HtmlTargetElement("action")]
    public class ActionHelper : TagHelper
    {
        public string Type { set; get; } = "button";
        public string Text { set; get; } = "";
        public string Size { set; get; } = "";
        public string Theme { set; get; } = "";
        public bool Radius { set; get; } = false;
        public string Icon { set; get; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
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
