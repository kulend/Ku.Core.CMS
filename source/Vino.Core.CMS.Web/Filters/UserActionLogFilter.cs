using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog.Filters;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Extensions;

namespace Vino.Core.CMS.Web.Filters
{
    public class UserActionLogFilter : IActionFilter
    {
        private IUserActionLogService service;

        public UserActionLogFilter(IUserActionLogService service)
        {
            this.service = service;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //var result = ((JsonResult)context.Result).Value;
            var userId = context.HttpContext.User.GetUserIdOrZero();
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            var userActionAttr = action.MethodInfo.GetCustomAttribute<UserActionAttribute>(false);
            //var result = context.Result as JsonResult;
            if (userActionAttr != null)
            {
                UserActionLogDto log = new UserActionLogDto();
                log.Operation = userActionAttr.Operation;
                log.ControllerName = action.ControllerName;
                log.ActionName = action.ActionName;
                log.UserId = userId;
                log.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                log.Url = context.HttpContext.Request.Path;
                //log.ActionResult = result?.Value.ToString();
                service.AddAsync(log);
            }
        }
    }
}
