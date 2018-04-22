//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：FunctionDto.cs
// 功能描述：功能 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class FunctionDto : BaseProtectedDto
    {
        /// <summary>
        /// 父功能
        /// </summary>
        [DataType("Hidden")]
        public long? ParentId { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name= "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "权限码")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; }

        /// <summary>
        /// 是否有子功能
        /// </summary>
        public bool HasSub { set; get; } = false;

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 角色是否有该功能权限
        /// </summary>
        public bool IsRoleHasAuth { set; get; } = false;
    }
}
