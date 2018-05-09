//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxUserController.cs
// 功能描述：微信用户 后台访问控制类
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
using Ku.Core.CMS.Web.Security;
using Ku.Core.Infrastructure.Exceptions;

namespace Ku.Core.CMS.Web.Backend.Areas.WeChat.Views.WxUser
{
    [Area("WeChat")]
    [Auth("wechat.user")]
    public class WxUserController : BackendController
    {
        private IWxUserService service;
        private IWxUserTagService tagService;
        private IWxAccountService accountService;

        public WxUserController(IWxUserService _service, IWxUserTagService _tagService, IWxAccountService _accountService)
        {
            this.service = _service;
            this.tagService = _tagService;
            this.accountService = _accountService;
        }

        #region 微信用户

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            //取得公众号数据
            var accounts = await accountService.GetListAsync(null, "Name asc");
            ViewBag.Accounts = accounts;
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetUserList(int page, int rows, WxUserSearch where)
        {
            var data = await service.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("view")]
        public async Task<IActionResult> Detail(long id)
        {
            var module = await service.GetByIdAsync(id);
            if (module == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            return View(module);
        }

        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Auth("remark")]
        public async Task<IActionResult> Remark(long id)
        {
            var module = await service.GetByIdAsync(id);
            if (module == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }
            return View(module);
        }

        /// <summary>
        /// 保存备注
        /// </summary>
        [HttpPost]
        [Auth("remark")]
        public async Task<IActionResult> SaveRemark(WxUserDto model)
        {
            await service.SaveRemarkAsync(model);
            return JsonData(true);
        }

        #endregion

        #region 用户标签

        [Auth("tag.view")]
        public async Task<IActionResult> TagList()
        {
            //取得公众号数据
            var accounts = await accountService.GetListAsync(null, "Name asc");
            ViewBag.Accounts = accounts;
            return View();
        }

        [Auth("tag.view")]
        public async Task<IActionResult> GetTagList(int page, int rows, WxUserTagSearch where)
        {
            var data = await tagService.GetListAsync(page, rows, where, "TagId asc");
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("tag.edit")]
        public async Task<IActionResult> TagEdit(long? id, long? AccountId)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await tagService.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }
                model.Account = await accountService.GetByIdAsync(model.AccountId);
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
                WxUserTagDto dto = new WxUserTagDto();
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
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        [HttpPost]
        [Auth("tag.edit")]
        public async Task<IActionResult> SaveTag(WxUserTagDto model)
        {
            await tagService.SaveAsync(model);
            return JsonData(true);
        }

        [HttpPost]
        [Auth("tag.delete")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            await tagService.DeleteAsync(id);
            return JsonData(true);
        }

        #endregion

        #region 数据同步

        [Auth("sync")]
        public async Task<IActionResult> UserSync(long AccountId)
        {
            var account = await accountService.GetByIdAsync(AccountId);
            if (account == null)
            {
                throw new VinoDataNotFoundException("数据出错!");
            }
            return View(account);
        }

        [Auth("sync")]
        public async Task<IActionResult> DoSync(WxAccountDto model)
        {
            await service.SyncAsync(model.Id);
            return JsonData(true);
        }

        #endregion
    }
}
