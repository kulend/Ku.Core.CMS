//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MenuDto.cs
// 功能描述：菜单 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class MenuDto : BaseDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        [DataType("Hidden")]
        public long? ParentId { get; set; }

        public MenuDto Parent { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "权限编码")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "菜单链接")]
        public string Url { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DefaultValue(0)]
        [Display(Name = "排序值")]
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// 是否显示
        /// </summary>
        [Display(Name = "是否显示", Prompt = "是|否")]
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public bool HasSubMenu { set; get; }

        public ICollection<MenuDto> SubMenus { set; get; }

    }
}
