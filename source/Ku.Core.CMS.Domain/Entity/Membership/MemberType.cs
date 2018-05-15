//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberType.cs
// 功能描述：会员类型 实体类
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

namespace Ku.Core.CMS.Domain.Entity.Membership
{
    /// <summary>
    /// 会员类型
    /// </summary>
    [Table("membership_member_type")]
    public class MemberType : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { set; get; }
    }

    /// <summary>
    /// 会员类型 检索条件
    /// </summary>
    public class MemberTypeSearch : BaseProtectedSearch<MemberType>
    {

    }
}
