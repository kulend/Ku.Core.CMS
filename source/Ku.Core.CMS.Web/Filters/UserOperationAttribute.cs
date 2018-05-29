using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Ku.Core.CMS.Data.Common;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Web.Extensions;
using Ku.Core.EventBus;
using Ku.Core.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Ku.Core.CMS.Web.Filters
{
    /// <summary>
    /// 用户操作记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UserOperationAttribute : VinoActionAttribute
    {
        public string Operation { set; get; }

        public UserOperationAttribute()
        {
        }

        public UserOperationAttribute(string operation)
        {
            this.Operation = operation;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            SaveLog(context, context.Result);
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            SaveLog(context, context.Result);
        }

        private void SaveLog(ActionContext context, IActionResult result)
        {
            //var userId = context.HttpContext.User.GetUserIdOrZero();
            //UserActionLogDto log = new UserActionLogDto();
            //log.Operation = Operation;
            //log.ControllerName = "";
            //log.ActionName = "";
            //log.UserId = userId;
            //log.Ip = context.HttpContext.IpAddress();
            //log.Url = context.HttpContext.RequestPath();
            //log.UrlReferrer = context.HttpContext.UrlReferrer();
            //log.UserAgent = context.HttpContext.UserAgent().Substr(0, 250);
            //log.Method = context.HttpContext.Request.Method;
            //log.QueryString = context.HttpContext.Request.QueryString.ToString().Substr(0, 250);
            //if (result is JsonResult r1)
            //{
            //    log.ActionResult = r1.Value.JsonSerialize().SubstringByByte(480);
            //}
            //else if (result is FileContentResult file)
            //{
            //    log.ActionResult = file.FileDownloadName;
            //}
            //log.CreateTime = DateTime.Now;
            //var _dbContext = context.HttpContext.RequestServices.GetService<IDbContext>();
            //var _publisher = context.HttpContext.RequestServices.GetService<IEventPublisher>();
            //using (var trans = _dbContext.Database.BeginTransaction())
            //{
            //    _publisher.Publish(log);
            //    trans.Commit();
            //}
        }
    }
}
