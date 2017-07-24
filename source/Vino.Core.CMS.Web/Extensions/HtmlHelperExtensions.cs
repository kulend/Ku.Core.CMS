using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent LayuiTextBoxFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiTextBoxFor(expression);
        }

        public static IHtmlContent LayuiSwitchFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiSwitchFor(expression);
        }

        public static HtmlString LayuiFormSubmitAndClose(this IHtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<div class=\"layui-form-item\">");
            sb.AppendLine($"<div class=\"layui-input-block\">");
            sb.AppendLine("<button class=\"layui-btn\" lay-submit>保 存</button>");
            sb.AppendLine("<button type=\"button\" class=\"layui-btn layui-btn-warm\" action=\"javascript:closeWindow()\">关 闭</button>");
            sb.AppendLine("</div></div>");

            return new HtmlString(sb.ToString());
        }

        public static MvcForm LayuiBeginForm<TModel>(this IHtmlHelper htmlHelper, string action)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiBeginForm(action);
        }

        public static IHtmlContent LayuiTextAreaFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiTextAreaFor(expression);
        }

        public static IHtmlContent LayuiHiddenFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiHiddenFor(expression);
        }

        public static IHtmlContent LayuiPasswordFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var helper = htmlHelper as VinoHtmlHelper<TModel>;
            return helper.LayuiPasswordFor(expression);
        }

    }
}
