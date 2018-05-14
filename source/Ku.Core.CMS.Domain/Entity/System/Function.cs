//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Function.cs
// 功能描述：功能 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ku.Core.CMS.Domain.Entity.System
{
    /// <summary>
    /// 功能
    /// </summary>
    [Table("system_function")]
    public class Function : BaseEntity
    {
        /// <summary>
        /// 父功能
        /// </summary>
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
        [Display(Name = "是否可用")]
        public bool IsEnable { set; get; }

        /// <summary>
        /// 是否有子功能
        /// </summary>
        public bool HasSub { set; get; } = false;

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { set; get; }
    }

    /// <summary>
    /// 功能 检索条件
    /// </summary>
    public class FunctionSearch : BaseSearch<Function>
    {
        /// <summary>
        /// 父功能
        /// </summary>
        public long? ParentId { get; set; }
    }
}
