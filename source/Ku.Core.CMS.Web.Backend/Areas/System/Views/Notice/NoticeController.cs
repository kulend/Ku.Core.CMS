//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：NoticeController.cs
// 功能描述：公告 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-18 09:55
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Web.Security;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.CMS.Domain.Entity.System;

namespace Ku.Core.CMS.Web.Backend.Areas.System.Views.Notice
{
    [Area("System")]
    [Auth("system.notice")]
    public class NoticeController : BackendController
    {
        private readonly INoticeService _service;

        public NoticeController(INoticeService service)
        {
            this._service = service;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, NoticeSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, "StickyNum desc,PublishedTime desc");
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                NoticeDto dto = new NoticeDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(NoticeDto model)
        {
            await _service.SaveAsync(model);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(params long[] id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("restore")]
        public async Task<IActionResult> Restore(params long[] id)
        {
            await _service.RestoreAsync(id);
            return JsonData(true);
        }

        [Auth("show")]
        public async Task<IActionResult> NoticeDialog()
        {
            var data = await _service.GetListAsync(1, 10, new NoticeSearch { IsDeleted = false, IsPublished = true}, "StickyNum desc,PublishedTime desc");
            return View(data.items);
        }
    }
}
