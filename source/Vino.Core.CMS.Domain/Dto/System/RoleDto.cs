//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：RoleDto.cs
// 功能描述：角色 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class RoleDto : BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 名称(英文)
        /// </summary>
        [Required, MaxLength(40)]
        [Display(Name = "英文名")]
        public string NameEn { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用", Prompt = "是|否")]
        public bool IsEnable { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
