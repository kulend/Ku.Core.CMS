using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_function_module")]
    public class FunctionModule: BaseEntity
    {
        public FunctionModule()
        {
            this.Childrens = new List<FunctionModule>();
            this.Actions = new List<FunctionModuleAction>();
        }

        /// <summary>
        /// 父模块
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Url相对路径
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(20)]
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
        public bool IsMenu { get; set; }

        /// <summary>
        /// 是否包含操作码
        /// </summary>
        public bool HasCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }

        public FunctionModule Parent { get; set; }

        public ICollection<FunctionModuleAction> Actions { get; set; }

        public ICollection<FunctionModule> Childrens { get; set; }
    }
}
