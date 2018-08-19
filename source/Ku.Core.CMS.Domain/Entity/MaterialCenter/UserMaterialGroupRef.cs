//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserMaterialGroup.cs
// 功能描述：用户素材分组 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-18 11:31
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.Domain.Enum.MaterialCenter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.MaterialCenter
{
    /// <summary>
    /// 用户素材分组关联
    /// </summary>
    [Table("materialcenter_user_material_group_ref")]
    public class UserMaterialGroupRef
    {
        public long GroupId { set; get; }

        public virtual UserMaterialGroup Group { set; get; }

        public long MaterialId { set; get; }
    }
}
