using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Service.System.Dto
{
    public class MenuDto
    {
        public long Id { get; set; }

        public DateTime CreateTime { set; get; }

        public bool IsDeleted { set; get; } = false;

        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        public short Type { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// 菜单备注
        /// </summary>
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
