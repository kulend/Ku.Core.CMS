using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.WeChat;
using Vino.Core.CMS.Service.WeChat;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Web.Backend.Areas.WeChat.Views.User
{
    [Area("WeChat")]
    [Auth("wechat.user")]
    public class UserController : BackendController
    {
        private IWxUserTagService tagService;
        private IWxAccountService accountService;

        public UserController(IWxUserTagService _tagService, IWxAccountService _accountService)
        {
            this.tagService = _tagService;
            this.accountService = _accountService;
        }

        #region 用户标签

        [Auth("tag.view")]
        public async Task<IActionResult> TagList()
        {
            //取得公众号数据
            var accounts = await accountService.GetAllAsync();
            ViewBag.Accounts = accounts;
            return View();
        }

        [Auth("tag.view")]
        public async Task<IActionResult> GetTagList(int page, int rows, WxUserTagSearch where)
        {
            var data = await tagService.GetListAsync(page, rows, where);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("tag.edit")]
        public async Task<IActionResult> EditTag(long? id)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await tagService.GetByIdAsync(id.Value);
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
                WxUserTagDto dto = new WxUserTagDto();
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

        #endregion
    }
}
