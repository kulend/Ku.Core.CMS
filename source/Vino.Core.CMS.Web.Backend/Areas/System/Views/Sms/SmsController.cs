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
        private readonly ISmsAccountService _accountService;

        public SmsController(ISmsAccountService accountService)
        {
            this._accountService = accountService;
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


    }
}
