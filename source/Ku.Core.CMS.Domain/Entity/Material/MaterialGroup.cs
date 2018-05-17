//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MaterialGroup.cs
// 功能描述：素材分组 实体类
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
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using Ku.Core.CMS.Domain.Enum.Material;

namespace Ku.Core.CMS.Domain.Entity.Material
{
    /// <summary>
    /// 素材分组
    /// </summary>
    [Table("material_group")]
    public class MaterialGroup : BaseProtectedEntity
    {
        /// <summary>
        /// 分组类型
        /// </summary>
        public EmMaterialGroupType Type { set; get; } = EmMaterialGroupType.Picture;

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required, MaxLength(30)]
        public string Name { set; get; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public long CreateUserId { set; get; }
    }

    /// <summary>
    /// 素材分组 检索条件
    /// </summary>
    public class MaterialGroupSearch : BaseProtectedSearch<MaterialGroup>
    {

    }
}
