//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Role.cs
// 功能描述：角色 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ku.Core.CMS.Domain.Entity.System
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("system_role")]
    public class Role : BaseProtectedEntity
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
        [DefaultValue(true)]
        [Display(Name = "是否启用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 角色 检索条件
    /// </summary>
    public class RoleSearch : BaseProtectedSearch<Role>
    {

    }
}
