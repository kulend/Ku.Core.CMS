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
using Vino.Core.CMS.Service.System;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Controllers
{
    [Area("System")]
    [Authorize]
    public class UserController : BaseController
    {
        private IIocResolver _ioc;

        public UserController(IIocResolver ioc)
        {
            this._ioc = ioc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUserList(int? page, int? rows)
        {
            if (!page.HasValue || page.Value < 1)
            {
                page = 1;
            }
            if (!rows.HasValue || rows.Value < 1)
            {
                rows = 10;
            }

            var service = _ioc.Resolve<IUserService>();
            var list = service.GetUserList(page.Value, rows.Value, out int count);
            return PagerData(list, page.Value, rows.Value, count);
        }

        public IActionResult Edit(long? id)
        {
            var service = _ioc.Resolve<IUserService>();
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
                UserDto dto = new UserDto();
                dto.IsEnable = true;
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        [HttpPost]
        public IActionResult Save(UserDto model)
        {
            var service = _ioc.Resolve<IUserService>();
            service.SaveUser(model);
            return Json(new { code = 0 });
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        [HttpPost]
        public IActionResult Disable(long id)
        {
            var service = _ioc.Resolve<IUserService>();

            return Json(new { code = 0 });
        }
    }
}
