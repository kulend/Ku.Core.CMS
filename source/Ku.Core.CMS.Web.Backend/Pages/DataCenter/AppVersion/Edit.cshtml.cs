using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Pages.DataCenter.AppVersion
{
    /// <summary>
    /// 应用版本 编辑页面
    /// </summary>
    [Auth("datacenter.appversion")]
    public class EditModel : BasePage
    {
        private readonly IAppVersionService _service;
        private readonly IAppService _appService;

        public EditModel(IAppVersionService service, IAppService appService)
        {
            this._service = service;
            _appService = appService;
        }

        [BindProperty]
        public AppVersionDto Dto { set; get; }

        /// <summary>
        /// 取得数据
        /// </summary>
        [Auth("edit")]
        public async Task OnGetAsync(long? id, long? AppId)
        {
            if (id.HasValue)
            {
                Dto = await _service.GetByIdAsync(id.Value);
                if (Dto == null)
                {
                    throw new VinoDataNotFoundException();
                }

                //取得app信息
                var appdto = await _appService.GetByIdAsync(Dto.AppId);
                if (appdto == null)
                {
                    throw new VinoDataNotFoundException("无法取得应用数据!");
                }
                Dto.App = appdto;

                ViewData["Mode"] = "Edit";
            }
            else
            {
                if (!AppId.HasValue)
                {
                    throw new VinoArgNullException("缺少参数AppId！");
                }
                //取得app信息
                var appdto = await _appService.GetByIdAsync(AppId.Value);
                if (appdto == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                Dto = new AppVersionDto();
                Dto.AppId = appdto.Id;
                Dto.App = appdto;

                ViewData["Mode"] = "Add";
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [Auth("edit")]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                throw new VinoArgNullException();
            }

            await _service.SaveAsync(Dto);
            return Json(true);
        }
    }
}
