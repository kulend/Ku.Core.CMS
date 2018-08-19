//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserMaterialGroupDto.cs
// 功能描述：用户素材分组 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-18 11:31
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Enum.MaterialCenter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.MaterialCenter
{
    /// <summary>
    /// 用户素材分组
    /// </summary>
    public class UserMaterialGroupDto : BaseDto
    {
        public long UserId { set; get; }

        [Display(Name = "分组类型")]
        public EmUserMaterialGroupType Type { set; get; }

        [Display(Name = "分组名称")]
        [Required, StringLength(32)]
        public string Name { set; get; }
    }
}
