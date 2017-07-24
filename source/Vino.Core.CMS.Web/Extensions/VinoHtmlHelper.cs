using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using NLog.Web.LayoutRenderers;
using Vino.Core.CMS.Core.Extensions;

namespace Microsoft.AspNetCore.Mvc.Rendering
{
    public class VinoHtmlHelper<TModel>: HtmlHelper<TModel>
    {
        private readonly IHtmlGenerator _htmlGenerator;
        private readonly HtmlEncoder _htmlEncoder;
        public VinoHtmlHelper(IHtmlGenerator htmlGenerator, 
            ICompositeViewEngine viewEngine, 
            IModelMetadataProvider metadataProvider, 
            IViewBufferScope bufferScope, 
            HtmlEncoder htmlEncoder, 
            UrlEncoder urlEncoder, 
            ExpressionTextCache expressionTextCache) : 
            base(htmlGenerator, viewEngine, metadataProvider, bufferScope, htmlEncoder, urlEncoder, expressionTextCache)
        {
            this._htmlGenerator = htmlGenerator;
            this._htmlEncoder = htmlEncoder;
        }

        public IHtmlContent LayuiTextBoxFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var tag =  new TagBuilder("div");
            tag.AddCssClass("layui-form-item");

            var modelExplorer = GetModelExplorer(expression);
            var metadata = modelExplorer.Metadata;
            var displayName = metadata.GetDisplayName();
            var isRequired = metadata.IsRequired;
            var placeholder = metadata.Placeholder;
            var name = GetExpressionName(expression);
            //取得最大长度
            var maxLength = 30;
            var maxLengthAttribute = metadata.ValidatorMetadata.SingleOrDefault(x => x.GetType() == typeof(MaxLengthAttribute));
            if (maxLengthAttribute != null)
            {
                maxLength = (maxLengthAttribute as MaxLengthAttribute).Length;
            }
            var isNumber = metadata.ModelType == typeof(Int32) || metadata.ModelType == typeof(Int16);
            var input = new TagBuilder("input");
            
            //长度标签
            if (isNumber)
            {
                input.AddCssClass("input-length-num");
            }
            else if (maxLength >= 50)
            {
                input.AddCssClass("input-length-long");
            }else if (maxLength >= 20)
            {
                input.AddCssClass("input-length-middle");
            }
            else
            {
                input.AddCssClass("input-length-short");
            }

            input.AddCssClass("layui-input");
            input.TagRenderMode = TagRenderMode.SelfClosing;
            input.MergeAttribute("type", InputType.Text.ToString());
            input.MergeAttribute("name", name, replaceExisting: true);
            if (placeholder.IsNotNullOrEmpty())
            {
                input.MergeAttribute("placeholder", placeholder);
            }
            if (isRequired)
            {
                input.MergeAttribute("lay-verify", "required");
            }

            if (!isNumber)
            {
                input.MergeAttribute("maxlength", maxLength.ToString());
            }

            if (modelExplorer.Model != null)
            {
                input.MergeAttribute("value", modelExplorer.Model.ToString());
            }

            tag.InnerHtml.AppendHtml($"<label class=\"layui-form-label\">{displayName}</label>");
            tag.InnerHtml.AppendHtml("<div class=\"layui-input-block\">");
            tag.InnerHtml.AppendHtml(input);
            tag.InnerHtml.AppendHtml("</div>");
            return tag;
        }

        public IHtmlContent LayuiSwitchFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var tag = new TagBuilder("div");
            tag.AddCssClass("layui-form-item");

            var modelExplorer = GetModelExplorer(expression);
            var metadata = modelExplorer.Metadata;
            var displayName = metadata.GetDisplayName();
            //var isRequired = metadata.IsRequired;
            var placeholder = metadata.Placeholder;
            var name = GetExpressionName(expression);
            var input = new TagBuilder("input");
            input.GenerateId(name, "");
            input.TagRenderMode = TagRenderMode.SelfClosing;
            input.MergeAttribute("type", InputType.CheckBox.ToString());
            input.MergeAttribute("name", name, replaceExisting: true);
            input.MergeAttribute("value", "true");
            input.MergeAttribute("lay-skin", "switch");
            if (placeholder.IsNotNullOrEmpty())
            {
                input.MergeAttribute("lay-text", placeholder);
            }
            if (modelExplorer.Model != null)
            {
                if (bool.TryParse(modelExplorer.Model.ToString(), out bool modelChecked))
                {
                    if (modelChecked)
                    {
                        input.MergeAttribute("checked", "checked");
                    }
                }
            }

            tag.InnerHtml.AppendHtml($"<label class=\"layui-form-label\">{displayName}</label>");
            tag.InnerHtml.AppendHtml("<div class=\"layui-input-inline\">");
            tag.InnerHtml.AppendHtml(input);
            tag.InnerHtml.AppendHtml("</div>");
            return tag;
        }

        public MvcForm LayuiBeginForm(string action, FormMethod method = FormMethod.Post)
        {
            ViewContext.FormContext = new FormContext
            {
                CanRenderAtEndOfForm = true
            };

            var tagBuilder = new TagBuilder("form");
            tagBuilder.GenerateId("inputForm", "");
            tagBuilder.AddCssClass("layui-form");
            tagBuilder.MergeAttribute("action", action);
            tagBuilder.MergeAttribute("method", method.ToString(), replaceExisting: true);

            tagBuilder.TagRenderMode = TagRenderMode.StartTag;
            tagBuilder.WriteTo(ViewContext.Writer, _htmlEncoder);

            return new MvcForm(ViewContext, _htmlEncoder);
        }

        public IHtmlContent LayuiTextAreaFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var tag = new TagBuilder("div");
            tag.AddCssClass("layui-form-item");

            var modelExplorer = GetModelExplorer(expression);
            var metadata = modelExplorer.Metadata;
            var displayName = metadata.GetDisplayName();
            var isRequired = metadata.IsRequired;
            var placeholder = metadata.Placeholder;
            var name = GetExpressionName(expression);
            //取得最大长度
            var maxLength = 100;
            var maxLengthAttribute = metadata.ValidatorMetadata.SingleOrDefault(x => x.GetType() == typeof(MaxLengthAttribute));
            if (maxLengthAttribute != null)
            {
                maxLength = (maxLengthAttribute as MaxLengthAttribute).Length;
            }
            var input = new TagBuilder("textarea");

            input.AddCssClass("layui-textarea");
            
            input.TagRenderMode = TagRenderMode.Normal;
            input.MergeAttribute("name", name, replaceExisting: true);
            if (placeholder.IsNotNullOrEmpty())
            {
                input.MergeAttribute("placeholder", placeholder);
            }
            if (isRequired)
            {
                input.MergeAttribute("lay-verify", "required");
            }
            input.MergeAttribute("maxlength", maxLength.ToString());
            if (modelExplorer.Model != null)
            {
                input.InnerHtml.Append(modelExplorer.Model.ToString());
            }

            tag.InnerHtml.AppendHtml($"<label class=\"layui-form-label\">{displayName}</label>");
            tag.InnerHtml.AppendHtml("<div class=\"layui-input-block\">");
            tag.InnerHtml.AppendHtml(input);
            tag.InnerHtml.AppendHtml("</div>");
            return tag;
        }

        public IHtmlContent LayuiHiddenFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var modelExplorer = GetModelExplorer(expression);
            var name = GetExpressionName(expression);

            var input = new TagBuilder("input");
            input.GenerateId(name, "");
            input.TagRenderMode = TagRenderMode.SelfClosing;
            input.MergeAttribute("type", InputType.Hidden.ToString());
            input.MergeAttribute("name", name, replaceExisting: true);

            if (modelExplorer.Model != null)
            {
                input.MergeAttribute("value", modelExplorer.Model.ToString());
            }
            return input;
        }

        public IHtmlContent LayuiPasswordFor<TResult>(Expression<Func<TModel, TResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var tag = new TagBuilder("div");
            tag.AddCssClass("layui-form-item");

            var modelExplorer = GetModelExplorer(expression);
            var metadata = modelExplorer.Metadata;
            var displayName = metadata.GetDisplayName();
            var isRequired = metadata.IsRequired;
            var placeholder = metadata.Placeholder;
            var name = GetExpressionName(expression);
            //取得最大长度
            var maxLength = 20;
            var maxLengthAttribute = metadata.ValidatorMetadata.SingleOrDefault(x => x.GetType() == typeof(MaxLengthAttribute));
            if (maxLengthAttribute != null)
            {
                maxLength = (maxLengthAttribute as MaxLengthAttribute).Length;
            }

            var input = new TagBuilder("input");

            //长度标签
            input.AddCssClass("input-length-short");

            input.AddCssClass("layui-input");
            input.TagRenderMode = TagRenderMode.SelfClosing;
            input.MergeAttribute("type", InputType.Password.ToString());
            input.MergeAttribute("name", name, replaceExisting: true);
            if (placeholder.IsNotNullOrEmpty())
            {
                input.MergeAttribute("placeholder", placeholder);
            }
            if (isRequired)
            {
                input.MergeAttribute("lay-verify", "required");
            }

            input.MergeAttribute("maxlength", maxLength.ToString());

            if (modelExplorer.Model != null)
            {
                input.MergeAttribute("value", modelExplorer.Model.ToString());
            }

            tag.InnerHtml.AppendHtml($"<label class=\"layui-form-label\">{displayName}</label>");
            tag.InnerHtml.AppendHtml("<div class=\"layui-input-block\">");
            tag.InnerHtml.AppendHtml(input);
            tag.InnerHtml.AppendHtml("</div>");
            return tag;
        }

    }
}
