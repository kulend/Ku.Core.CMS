using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.EventBus;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;

namespace Vino.Core.CMS.Web.Filters
{
    /// <summary>
    /// 用户操作日志记录
    /// </summary>
    public class UserActionLogFilter : IActionFilter
    {
        private readonly IEventPublisher _publisher;
        private readonly IDbContext _dbContext;
        public UserActionLogFilter(IDbContext dbContext, IEventPublisher publisher)
        {
            this._publisher = publisher;
            this._dbContext = dbContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var userId = context.HttpContext.User.GetUserIdOrZero();
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            var userActionAttr = action.MethodInfo.GetCustomAttribute<UserActionAttribute>(false);
            var result = context.Result as JsonResult;
            if (userActionAttr != null)
            {
                UserActionLogDto log = new UserActionLogDto();
                log.Operation = userActionAttr.Operation;
                log.ControllerName = action.ControllerName;
                log.ActionName = action.ActionName;
                log.UserId = userId;
                log.Ip = context.HttpContext.IpAddress();
                log.Url = context.HttpContext.RequestPath();
                log.UrlReferrer = context.HttpContext.UrlReferrer();
                log.UserAgent = context.HttpContext.UserAgent().Substr(0, 250);
                log.Method = context.HttpContext.Request.Method;
                log.QueryString = context.HttpContext.Request.QueryString.ToString().Substr(0, 250);
                if (result != null)
                {
                    var json = JsonConvert.SerializeObject(result.Value);
                    log.ActionResult = json.SubstringByByte(480);
                }
                log.CreateTime = DateTime.Now;
                using (var trans = _dbContext.Database.BeginTransaction())
                {
                    _publisher.Publish(log);
                    trans.Commit();
                }
            }
        }
    }
}
