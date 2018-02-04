//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
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
using Vino.Core.CMS.Domain.Entity.System;
using Vino.Core.CMS.Domain.Enum.Material;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Material
{
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

        /// <summary>
        /// 创建用户
        /// </summary>
        public User CreateUser { set; get; }
    }

    public class MaterialGroupSearch : BaseSearch<MaterialGroup>
    {

    }
}
