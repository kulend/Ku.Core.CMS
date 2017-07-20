using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_menu")]
    public class Menu : BaseProtectedEntity
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required, MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        [Required, MaxLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        public short Type { get; set; }

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
        /// 菜单备注
        /// </summary>
        [MaxLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; } = true;

        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public bool HasSubMenu { set; get; }
    }
}
