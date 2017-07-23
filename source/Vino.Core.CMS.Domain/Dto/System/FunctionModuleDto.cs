using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class FunctionModuleDto : BaseDto
    {
        /// <summary>
        /// 父模块
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// Url相对路径
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "相对路径", Prompt = "")]
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(20), Display(Name = "图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 是否最子级
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// 是否是导航菜单
        /// </summary>
        [Display(Name = "是否菜单", Prompt = "是|否")]
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否包含操作码
        /// </summary>
        public bool HasCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { get; set; }
    }
}
