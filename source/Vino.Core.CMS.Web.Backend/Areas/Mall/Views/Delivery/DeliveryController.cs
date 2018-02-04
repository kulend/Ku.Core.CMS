//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：DeliveryController.cs
// 功能描述：物流配送 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:05
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

namespace Vino.Core.CMS.Web.Backend.Areas.Mall.Views.Delivery
{
    [Area("Mall")]
    [Auth("mall.delivery")]
    public class DeliveryController : BackendController
    {
        private readonly IDeliveryService _service;

        public DeliveryController(IDeliveryService service)
        {
            this._service = service;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, DeliverySearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, null);
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
                DeliveryDto dto = new DeliveryDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(DeliveryDto model)
        {
            await _service.SaveAsync(model);
            return JsonData(true);
        }

        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }
    }
}
