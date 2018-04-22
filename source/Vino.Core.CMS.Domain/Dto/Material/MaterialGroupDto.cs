//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MaterialGroupDto.cs
// 功能描述：素材分组 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Dto.System;
using Vino.Core.CMS.Domain.Enum.Material;

namespace Vino.Core.CMS.Domain.Dto.Material
{
    public class MaterialGroupDto : BaseProtectedDto
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
        public UserDto CreateUser { set; get; }
    }
}
