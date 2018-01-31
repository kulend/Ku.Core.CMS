using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Views.Log
{
    [Area("System")]
    [Auth("sys.log")]
    public class LogController : BackendController
    {
        private IUserActionLogService service;

        public LogController(IUserActionLogService service)
        {
            this.service = service;
        }

        [Auth("view")]
        public IActionResult Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows)
        {
            var data = await service.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
        }


        [Auth("view")]
        public async Task<IActionResult> Detail(long id)
        {
            var module = await service.GetByIdAsync(id);
            if (module == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            return View(module);
        }
    }
}
