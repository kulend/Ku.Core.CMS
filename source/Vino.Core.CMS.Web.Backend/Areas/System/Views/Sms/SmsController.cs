//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：SmsController.cs
// 功能描述：短信账号 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-03-26 16:05
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
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.IService.System;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Web.Backend.Areas.System.Views.Sms
{
    [Area("System")]
    [Auth("system.sms")]
    public class SmsController : BackendController
    {
        private readonly ISmsService _service;
        private readonly ISmsAccountService _accountService;
        private readonly ISmsTempletService _templetService;
        private readonly ISmsQueueService _queueService;

        public SmsController(ISmsService service, 
            ISmsAccountService accountService, ISmsTempletService templetService, ISmsQueueService queueService)
        {
            this._service = service;
            this._accountService = accountService;
            this._templetService = templetService;
            this._queueService = queueService;
        }

        #region 短信账户

        [Auth("account.view")]
        public async Task<IActionResult> AccountList()
        {
            return View();
        }

        [Auth("account.view")]
        public async Task<IActionResult> GetAccountList(int page, int rows, SmsAccountSearch where)
        {
            var data = await _accountService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("account.edit")]
        public async Task<IActionResult> AccountEdit(long? id)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _accountService.GetByIdAsync(id.Value);
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
                SmsAccountDto dto = new SmsAccountDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("account.edit")]
        public async Task<IActionResult> AccountSave(SmsAccountDto model)
        {
            await _accountService.SaveAsync(model);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("account.delete")]
        public async Task<IActionResult> AccountDelete(params long[] id)
        {
            await _accountService.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("account.restore")]
        public async Task<IActionResult> AccountRestore(params long[] id)
        {
            await _accountService.RestoreAsync(id);
            return JsonData(true);
        }

        #endregion

        #region 短信模板

        [Auth("templet.view")]
        public async Task<IActionResult> TempletList()
        {
            return View();
        }

        [Auth("templet.view")]
        public async Task<IActionResult> GetTempletList(int page, int rows, SmsTempletSearch where)
        {
            var data = await _templetService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("templet.edit")]
        public async Task<IActionResult> TempletEdit(long? id)
        {
            //取得短信账号列表
            ViewBag.Accounts =  await _accountService.GetListAsync(new SmsAccountSearch { IsDeleted = false}, null);

            if (id.HasValue)
            {
                //编辑
                var model = await _templetService.GetByIdAsync(id.Value);
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
                SmsTempletDto dto = new SmsTempletDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("templet.edit")]
        public async Task<IActionResult> TempletSave(SmsTempletDto model)
        {
            await _templetService.SaveAsync(model);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("templet.delete")]
        public async Task<IActionResult> TempletDelete(params long[] id)
        {
            await _templetService.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("templet.restore")]
        public async Task<IActionResult> TempletRestore(params long[] id)
        {
            await _templetService.RestoreAsync(id);
            return JsonData(true);
        }

        #endregion

        #region 短信队列

        [Auth("queue.view")]
        public async Task<IActionResult> QueueList()
        {
            return View();
        }

        [Auth("queue.view")]
        public async Task<IActionResult> GetQueueList(int page, int rows, SmsQueueSearch where)
        {
            var data = await _queueService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("queue.view")]
        public async Task<IActionResult> Detail(long id)
        {
            var model = await _queueService.GetByIdAsync(id);
            if (model == null || model.IsDeleted)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }

            var sms = await _service.GetByIdAsync(model.SmsId);
            if (sms == null || model.IsDeleted)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            model.Sms = sms;
            return View(model);
        }

        [Auth("add")]
        public async Task<IActionResult> Add()
        {
            //取得短信模板列表
            ViewBag.Templets = await _templetService.GetListAsync(new SmsTempletSearch {  IsDeleted = false}, null);

            return View();
        }

        [Auth("add")]
        public async Task<IActionResult> DoAdd(SmsDto model)
        {
            await _service.AddAsync(model);
            return JsonData(true);
        }

        #endregion
    }
}
