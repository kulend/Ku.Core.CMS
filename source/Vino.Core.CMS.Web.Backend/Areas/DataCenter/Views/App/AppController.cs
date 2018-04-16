//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppController.cs
// 功能描述：应用 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
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
using Vino.Core.CMS.Domain.Dto.DataCenter;
using Vino.Core.CMS.IService.DataCenter;
using Vino.Core.CMS.Domain.Entity.DataCenter;

namespace Vino.Core.CMS.Web.Backend.Areas.DataCenter.Views.App
{
    [Area("DataCenter")]
    [Auth("datacenter.app")]
    public class AppController : BackendController
    {
        private readonly IAppService _service;
        private readonly IAppVersionService _versionService;

        public AppController(IAppService service, IAppVersionService versionService)
        {
            this._service = service;
            this._versionService = versionService;
        }

        #region 应用

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, AppSearch where)
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
                AppDto dto = new AppDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(AppDto model)
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

        #endregion

        #region 应用版本

        [Auth("version.view")]
        public async Task<IActionResult> VersionList()
        {
            //取得应用列表
            ViewBag.Apps = await _service.GetListAsync(new AppSearch { IsDeleted = false }, null);
            return View();
        }

        [Auth("version.view")]
        public async Task<IActionResult> GetVersionList(int page, int rows, AppVersionSearch where)
        {
            var data = await _versionService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("version.edit")]
        public async Task<IActionResult> VersionEdit(long? id, long? AppId)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _versionService.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                //取得app信息
                var appdto = await _service.GetByIdAsync(model.AppId);
                if (appdto == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                model.App = appdto;
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                if (!AppId.HasValue)
                {
                    throw new VinoArgNullException("缺少参数AppId！");
                }
                //取得app信息
                var appdto = await _service.GetByIdAsync(AppId.Value);
                if (appdto == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                //新增
                AppVersionDto dto = new AppVersionDto();
                dto.AppId = appdto.Id;
                dto.App = appdto;
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("version.edit")]
        public async Task<IActionResult> VersionSave(AppVersionDto model)
        {
            await _versionService.SaveAsync(model);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("version.delete")]
        public async Task<IActionResult> VersionDelete(params long[] id)
        {
            await _versionService.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("version.restore")]
        public async Task<IActionResult> VersionRestore(params long[] id)
        {
            await _versionService.RestoreAsync(id);
            return JsonData(true);
        }

        #endregion
    }
}
