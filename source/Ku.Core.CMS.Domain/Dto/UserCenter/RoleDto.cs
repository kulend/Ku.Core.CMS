//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：RoleDto.cs
// 功能描述：角色 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 11:27
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleDto : BaseProtectedDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 名称(英文)
        /// </summary>
        [Required, StringLength(40)]
        [Display(Name = "英文名")]
        public string NameEn { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }
    }
}
