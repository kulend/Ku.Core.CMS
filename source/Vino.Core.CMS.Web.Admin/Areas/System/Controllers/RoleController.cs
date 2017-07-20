using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Core.Exceptions;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Service.System;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Controllers
{
    [Area("System")]
    [Authorize]
    public class RoleController : Controller
    {
        private IIocResolver _ioc;

        public RoleController(IIocResolver ioc)
        {
            this._ioc = ioc;
        }

        public IActionResult Index(int? page, int? size)
        {
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }
            if (!size.HasValue || size.Value < 1)
            {
                size = 10;
            }

            var service = _ioc.Resolve<IRoleService>();
            var list = service.GetList(page.Value, size.Value, out int count);
            ViewData["page"] = page;
            ViewData["size"] = size;
            ViewData["count"] = count;
            ViewData["pages"] = Math.Ceiling(count * 1.0 / size.Value);
            return View(list);
        }

        public IActionResult Edit(long? id, long? pid)
        {
            var service = _ioc.Resolve<IRoleService>();
            if (id.HasValue)
            {
                //编辑
                var model = service.GetById(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得用户数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                RoleDto dto = new RoleDto();
                //dto.IsEnable = true;
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [HttpPost]
        public IActionResult Save(RoleDto model)
        {
            var service = _ioc.Resolve<IRoleService>();
            service.Save(model);
            return Json(new { code = 0 });
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var service = _ioc.Resolve<IRoleService>();
            service.Delete(id);
            return Json(new { code = 0 });
        }
    }
}
