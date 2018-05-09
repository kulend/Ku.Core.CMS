using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.WebApi.Controllers.V1.DataCenter.App
{
    /// <summary>
    /// 应用相关接口
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/datacenter/app/feedback")]
    public class AppFeedbackController : WebApiController
    {
        private readonly IAppFeedbackService _service;
        private readonly IAppService _appService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AppFeedbackController(IAppFeedbackService service, IAppService appService)
        {
            this._service = service;
            this._appService = appService;
        }

        /// <summary>
        /// 提交应用反馈
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> Post(string appkey, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new VinoArgNullException("反馈内容不能为空！");
            }
            //取得应用信息
            var app = await _appService.GetByAppkeyAsync(appkey);
            if (app == null)
            {
                throw new VinoDataNotFoundException("无法取得应用信息！");
            }

            AppFeedbackDto dto = new AppFeedbackDto {
                AppId = app.Id,
                Content = content,
                ProviderName = "测试"
            };
            await _service.SaveAsync(dto);

            return Json(true);
        }
    }
}