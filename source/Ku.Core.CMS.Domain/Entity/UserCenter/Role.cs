//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Role.cs
// 功能描述：角色 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 11:27
//
//----------------------------------------------------------------

using Dnc.Extensions.Dapper.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("usercenter_role")]
    public class Role : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(20)]
        public string Name { set; get; }

        /// <summary>
        /// 名称(英文)
        /// </summary>
        [Required, StringLength(40)]
        public string NameEn { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DefaultValue(true)]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remarks { get; set; }
    }

    /// <summary>
    /// 角色 检索条件
    /// </summary>
    public class RoleSearch : BaseProtectedSearch<Role>
    {
        [Condition(Operation = ConditionOperation.Custom, CustomSql = "EXISTS (SELECT * FROM usercenter_user_role ref WHERE ref.RoleId=m.Id AND ref.UserId=@value)")]
        public long? UserId { set; get; }
    }
}
