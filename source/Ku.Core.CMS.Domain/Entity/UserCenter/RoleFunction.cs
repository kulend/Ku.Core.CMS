//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：RoleFunction.cs
// 功能描述：角色功能关联 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-17 09:34
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 角色功能关联
    /// </summary>
    [Table("usercenter_role_function")]
    public class RoleFunction
    {
        public long RoleId { set; get; }

        public long FunctionId { set; get; }

        public virtual System.Function Function { set; get; }
    }

    /// <summary>
    /// 角色功能关联 检索条件
    /// </summary>
    public class RoleFunctionSearch : BaseSearch<RoleFunction>
    {

    }
}
