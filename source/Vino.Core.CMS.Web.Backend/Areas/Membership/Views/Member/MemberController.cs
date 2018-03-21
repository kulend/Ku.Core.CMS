//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberController.cs
// 功能描述：会员 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 20:18
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.CMS.Domain.Dto.Membership;
using Vino.Core.CMS.Web.Base;
using Vino.Core.CMS.Web.Security;
using Vino.Core.CMS.IService.Membership;

namespace Vino.Core.CMS.Web.Backend.Areas.Membership.Views.Member
{
    [Area("Membership")]
    [Auth("membership.member")]
    public class MemberController : BackendController
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
        public async Task<IActionResult> GetList(int page, int rows)
        {
            var data = await _service.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("edit")]
        public async Task<IActionResult> Edit(long? id)
        {
            //取得会员类型列表
            ViewBag.MemberTypeList = await _memberTypeService.GetListAsync(null, null);

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

        #region 会员类型

        [Auth("type.view")]
        public IActionResult TypeList()
        {
            return View();
        }

        [Auth("type.view")]
        public async Task<IActionResult> GetTypeList(int page, int rows)
        {
            var data = await _memberTypeService.GetListAsync(page, rows, null, null);
            return PagerData(data.items, page, rows, data.count);
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

        /// <summary>
        /// 删除
        /// </summary>
        [HttpPost]
        [Auth("type.delete")]
        public async Task<IActionResult> TypeDelete(params long[] id)
        {
            await _memberTypeService.DeleteAsync(id);
            return JsonData(true);
        }

        /// <summary>
        /// 恢复
        /// </summary>
        [HttpPost]
        [Auth("type.restore")]
        public async Task<IActionResult> TypeRestore(params long[] id)
        {
            await _memberTypeService.RestoreAsync(id);
            return JsonData(true);
        }

        #endregion
    }
}
