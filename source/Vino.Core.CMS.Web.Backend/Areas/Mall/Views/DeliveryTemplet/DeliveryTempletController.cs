//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：DeliveryTempletController.cs
// 功能描述：配送模板 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-05 10:25
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Base;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Domain.Dto.Mall;
using Vino.Core.CMS.IService.Mall;
using Vino.Core.CMS.Domain.Entity.Mall;

namespace Vino.Core.CMS.Web.Backend.Areas.Mall.Views.DeliveryTemplet
{
    [Area("Mall")]
    [Auth("mall.deliverytemplet")]
    public class DeliveryTempletController : BackendController
    {
        private readonly IDeliveryTempletService _service;

        public DeliveryTempletController(IDeliveryTempletService service)
        {
            this._service = service;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, DeliveryTempletSearch where)
        {
            if (where == null)
            {
                where = new DeliveryTempletSearch();
            }
            if (!where.IsSnapshot.HasValue)
            {
                where.IsSnapshot = false;
            }
            var data = await _service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("view")]
        public async Task<IActionResult> Detail(long id)
        {
            //编辑
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            return View(model);
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
                DeliveryTempletDto dto = new DeliveryTempletDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(DeliveryTempletDto model)
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
    }
}
