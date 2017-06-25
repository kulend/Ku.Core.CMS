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
using Vino.Core.CMS.Service.System.Dto;

namespace Vino.Core.CMS.Web.Admin.Areas.System.Controllers
{
    [Area("System")]
    [Authorize]
    public class UserController : Controller
    {
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

            var service = IoC.Resolve<IUserService>();
            var list = service.GetUserList(page.Value, size.Value, out int count);
            ViewData["page"] = page;
            ViewData["size"] = size;
            ViewData["count"] = count;
            ViewData["pages"] = Math.Ceiling(count * 1.0 / size.Value);
            return View(list);
        }

        public IActionResult Edit(long? id, long? pid)
        {
            var service = IoC.Resolve<IUserService>();
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
            var service = IoC.Resolve<IUserService>();
            service.SaveUser(model);
            return Json(new { code = 0 });
        }
    }
}
