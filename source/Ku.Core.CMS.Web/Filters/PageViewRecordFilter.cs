using Ku.Core.CMS.Web.Attributes;
using Ku.Core.CMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.Infrastructure.Extensions;

namespace Ku.Core.CMS.Web.Filters
{
    public class PageViewRecordFilter : IAsyncPageFilter
    {
        private readonly IPageViewRecordService _service;

        public PageViewRecordFilter(IPageViewRecordService service)
        {
            _service = service;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            await next();

            await Task.Factory.StartNew(async () => {
                var method = context.GetHandlerMethod();
                var attr = method.GetCustomAttribute<PageViewRecordAttribute>(true);
                if (attr != null)
                {
                    await _service.SaveAsync(new PageViewRecordDto {
                        SessionId = context.HttpContext.Session.Id,
                        PageName = attr.PageName,
                        Url = context.HttpContext.RequestPath(),
                        Referer = context.HttpContext.UrlReferrer(),
                        UserAgent = context.HttpContext.UserAgent().SubstringByByte(128),
                        ClientIp = context.HttpContext.IpAddress(),
                    });
                }
            });
        }

        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
           await Task.CompletedTask;
        }
    }
}
