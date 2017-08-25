using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Service.Membership;
using Vino.Core.CMS.Web.Security;

namespace Vino.Core.CMS.Web.Admin.Areas.Membership.Views.Member
{
    [Area("Membership")]
    [Auth("membership.member")]
    public class MemberController : BaseController
    {
        private readonly IMemberService _service;
        private readonly IMemberTypeService _memberTypeService;

        public MemberController(IMemberService service, IMemberTypeService memberTypeService)
        {
            this._service = service;
            this._memberTypeService = memberTypeService;
        }

        #region 会员

        [Auth("view")]
        public IActionResult Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int limit)
        {
            var data = await _service.GetListAsync(page, limit);
            return PagerData(data.items, page, limit, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            //取得会员类型列表
            ViewBag.MemberTypeList = await _memberTypeService.GetAllAsync();

            if (id.HasValue)
            {
                //编辑
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员数据!");
                }
                model.Password = "the password has not changed";
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                MemberDto dto = new MemberDto();
                dto.IsEnable = true;
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [HttpPost]
        [Auth("edit")]
        public async Task<IActionResult> Save(MemberDto model)
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

        #endregion

        #region 会员类型

        [Auth("type.view")]
        public IActionResult TypeList()
        {
            return View();
        }

        [Auth("type.view")]
        public async Task<IActionResult> GetTypeList(int page, int limit)
        {
            var data = await _memberTypeService.GetListAsync(page, limit);
            return PagerData(data.items, page, limit, data.count);
        }

        [Auth("type.edit")]
        public async Task<IActionResult> TypeEdit(long? id)
        {
            if (id.HasValue)
            {
                //编辑
                var model = await _memberTypeService.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new VinoDataNotFoundException("无法取得会员类型数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                MemberTypeDto dto = new MemberTypeDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        [HttpPost]
        [Auth("type.edit")]
        public async Task<IActionResult> TypeSave(MemberTypeDto model)
        {
            await _memberTypeService.SaveAsync(model);
            return JsonData(true);
        }

        [HttpPost]
        [Auth("type.delete")]
        public async Task<IActionResult> TypeDelete(long id)
        {
            await _service.DeleteAsync(id);
            return JsonData(true);
        }

        #endregion
    }
}
