//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxQrcodeController.cs
// 功能描述：微信二维码 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 20:18
//
//----------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using Ku.Core.CMS.IService.WeChat;
using Ku.Core.CMS.Web.Base;
using Ku.Core.CMS.Web.Filters;
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Areas.WeChat.Views.WxQrcode
{
    [Area("WeChat")]
    [Auth("wechat.wxqrcode")]
    public class WxQrcodeController : BackendController
    {
        private IWxQrcodeService _service;
        private IWxAccountService _accountService;

        public WxQrcodeController(IWxQrcodeService service, IWxAccountService accountService)
        {
            this._service = service;
            this._accountService = accountService;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            //取得公众号数据
            var accounts = await _accountService.GetListAsync(null, "Name asc");
            ViewBag.Accounts = accounts;
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, WxQrcodeSearch where)
        {
            var data = await _service.GetListAsync(page, rows, where, "SceneId asc");
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id, long? AccountId)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                model.Account = await _accountService.GetByIdAsync(model.AccountId);
                if (model.Account == null)
                {
                    throw new VinoDataNotFoundException("数据出错!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                WxQrcodeDto dto = new WxQrcodeDto();
                if (AccountId.HasValue)
                {
                    dto.AccountId = AccountId.Value;
                    dto.Account = await _accountService.GetByIdAsync(AccountId.Value);
                    if (dto.Account == null)
                    {
                        throw new VinoDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    throw new VinoDataNotFoundException("参数出错!");
                }
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(WxQrcodeDto model)
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
