//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Menu.cs
// 功能描述：菜单 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.System
{
    [Table("system_menu")]
    public class Menu : BaseEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        public Menu Parent { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        [Required, MaxLength(64)]
        public string AuthCode { get; set; }

        /// <summary>
        /// 菜单标签
        /// </summary>
        [StringLength(20)]
        public string Tag { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [MaxLength(20)]
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DefaultValue(0)]
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public bool HasSubMenu { set; get; }

        public ICollection<Menu> SubMenus { set; get; }
    }

    public class MenuSearch : BaseProtectedSearch<Menu>
    {
        [SearchCondition(false)]
        public long? ParentId { set; get; }

    }
}
