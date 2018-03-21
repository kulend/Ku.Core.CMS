using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.Web.Base;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.IService.WeChat;
using Vino.Core.CMS.Domain.Entity.WeChat;

namespace Vino.Core.CMS.Web.Backend.Areas.WeChat.Views.WxMenu
{
    [Area("WeChat")]
    [Auth("wechat.menu")]
    public class WxMenuController : BackendController
    {
        private IWxMenuService service;
        private IWxAccountService accountService;
        private IWxUserTagService wxUserTagService;

        public WxMenuController(IWxMenuService _service, IWxAccountService _accountService, IWxUserTagService _wxUserTagService)
        {
            this.service = _service;
            this.accountService = _accountService;
            this.wxUserTagService = _wxUserTagService;
        }

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            //取得公众号数据
            var accounts = await accountService.GetListAsync(null, "Name asc");
            ViewBag.Accounts = accounts;
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, WxMenuSearch where)
        {
            var data = await service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id, long? AccountId)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                model.Account = await accountService.GetByIdAsync(model.AccountId);
                if (model.Account == null)
                {
                    throw new VinoDataNotFoundException("数据出错!");
                }
                //取得用户标签
                ViewBag.UserTags =  await wxUserTagService.GetListAsync(new WxUserTagSearch() { AccountId = model.AccountId }, "TagId asc");
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                WxMenuDto dto = new WxMenuDto();
                if (AccountId.HasValue)
                {
                    dto.AccountId = AccountId.Value;
                    dto.Account = await accountService.GetByIdAsync(AccountId.Value);
                    if (dto.Account == null)
                    {
                        throw new VinoDataNotFoundException("参数出错!");
                    }
                }
                else
                {
                    throw new VinoDataNotFoundException("参数出错!");
                }
                //取得用户标签
                ViewBag.UserTags = await wxUserTagService.GetListAsync(new WxUserTagSearch() { AccountId = dto.AccountId }, "TagId asc");
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(WxMenuDto model)
        {
            await service.SaveAsync(model);
            return JsonData(true);
        }

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("delete")]
        public async Task<IActionResult> Delete(params long[] id)
        {
            await service.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("restore")]
        public async Task<IActionResult> Restore(params long[] id)
        {
            await service.RestoreAsync(id);
            return JsonData(true);
        }
    }
}