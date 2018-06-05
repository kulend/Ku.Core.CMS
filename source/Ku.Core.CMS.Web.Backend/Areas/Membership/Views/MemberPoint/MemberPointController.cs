//----------------------------------------------------------------
// Copyright (C) 2018 ku 版权所有
//
// 文件名：MemberPointController.cs
// 功能描述：会员积分 后台访问控制类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 15:52
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ku.Core.CMS.Web.Security;
using Ku.Core.CMS.Web.Base;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.IService.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.Domain.Enum.Membership;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.CMS.Web.Extensions;

namespace Ku.Core.CMS.Web.Backend.Areas.Membership.Views.MemberPoint
{
    [Area("Membership")]
    [Auth("membership.member.point")]
    public class MemberPointController : BackendController
    {
        private readonly IMemberPointService _service;
        private readonly IMemberService _memberService;
        private readonly IMemberPointRecordService _recordService;

        public MemberPointController(IMemberPointService service, 
            IMemberService memberService,
            IMemberPointRecordService recordService)
        {
            this._service = service;
            this._memberService = memberService;
            this._recordService = recordService;
        }

        #region 会员积分

        [Auth("view")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Auth("view")]
        public async Task<IActionResult> GetList(int page, int rows, MemberPointSearch where)
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
                    throw new KuDataNotFoundException("无法取得数据!");
                }
                ViewData["Mode"] = "Edit";
                return View(model);
            }
            else
            {
                //新增
                MemberPointDto dto = new MemberPointDto();
                ViewData["Mode"] = "Add";
                return View(dto);
            }
        }

        ///// <summary>
        ///// 保存
        ///// </summary>
        //[HttpPost]
        //[Auth("edit")]
        //public async Task<IActionResult> Save(MemberPointDto model)
        //{
        //    await _service.SaveAsync(model);
        //    return JsonData(true);
        //}

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

        /// <summary>
        /// 积分调整
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Auth("adjust")]
        public async Task<IActionResult> Adjust(long? id, EmMemberPointType? type)
        {
            var members = new List<MemberDto>();
            MemberPointAdjustDto dto = new MemberPointAdjustDto();
            if (id.HasValue)
            {
                var model = await _service.GetByIdAsync(id.Value);
                if (model == null)
                {
                    throw new KuDataNotFoundException("无法取得数据!");
                }
                dto.Type = model.Type;
                var member = await _memberService.GetByIdAsync(model.MemberId);
                if (member != null)
                {
                    members.Add(member);
                }
            }
            else
            {
                if (!type.HasValue)
                {
                    throw new KuArgNullException("参数出错", "type");
                }

                dto.Type = type.Value;
            }

            ViewBag.Members = members;
            return View(dto);
        }

        /// <summary>
        /// 积分调整
        /// </summary>
        [HttpPost]
        [Auth("adjust")]
        public async Task<IActionResult> SaveAdjust(MemberPointAdjustDto dto)
        {
            await _service.AdjustAsync(dto.MemberId, dto.Type, dto.Points, 
                EmMemberPointBusType.AdminAdjust, 0, dto.Remark, User.GetUserIdOrZero());

            return JsonData(true);
        }

        #endregion

        #region 积分记录

        [Auth("record.view")]
        public async Task<IActionResult> RecordList(long id)
        {
            ViewData["MemberPointId"] = id;
            return View();
        }

        [Auth("record.view")]
        public async Task<IActionResult> GetRecordList(int page, int rows, MemberPointRecordSearch where)
        {
            var data = await _recordService.GetListAsync(page, rows, where, null);
            return PagerData(data.items, page, rows, data.count);
        }

        [Auth("record.view")]
        public async Task<IActionResult> RecordDetail(long id)
        {
            var model = await _recordService.GetByIdAsync(id);
            if (model == null)
            {
                throw new KuDataNotFoundException("无法取得数据!");
            }
            return View(model);
        }

        #endregion
    }
}
