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
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Extensions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;

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
            var userId = context.HttpContext.User.GetUserIdOrZero();
            var action = context.ActionDescriptor as ControllerActionDescriptor;
            if (action == null)
            {
                return;
            }
            var userActionAttr = action.MethodInfo.GetCustomAttribute<UserActionAttribute>(false);
            var result = context.Result as JsonResult;
            //Task.Factory.StartNew(() =>
            //{
                if (userActionAttr != null)
                {
                    UserActionLogDto log = new UserActionLogDto();
                    log.Id = ID.NewID();
                    log.Operation = userActionAttr.Operation;
                    log.ControllerName = action.ControllerName;
                    log.ActionName = action.ActionName;
                    log.UserId = userId;
                    log.Ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
                    log.Url = context.HttpContext.Request.Path;
                    var headersDictionary = context.HttpContext.Request.Headers;
                    var urlReferrer = headersDictionary[HeaderNames.Referer].ToString();
                    log.UrlReferrer = urlReferrer;
                    if (result != null)
                    {
                        var json = JsonConvert.SerializeObject(result.Value);
                        log.ActionResult = json.SubstringByByte(480);
                    }
                //using (var _db = new SecondaryVinoDbContext())
                //{
                //_db.UserActionLogs.Add(log);
                //_db.SaveChanges();
                //}
                service.AddAsync(log);
                }
            //});
        }
    }
}
