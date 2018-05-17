//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserRole.cs
// 功能描述：用户角色关联 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 13:01
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    [Table("usercenter_user_role")]
    public class UserRole
    {
        public long UserId { set; get; }
        public long RoleId { set; get; }

        public virtual Role Role { set; get; }
    }
}
