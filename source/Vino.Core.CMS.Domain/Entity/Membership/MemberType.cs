//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
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
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Membership
{
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

    public class MemberTypeSearch : BaseSearch<MemberType>
    {

    }
}
