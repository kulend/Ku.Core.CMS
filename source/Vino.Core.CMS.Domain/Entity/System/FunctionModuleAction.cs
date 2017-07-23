using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_function_module_action")]
    public class FunctionModuleAction : BaseEntity
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [Required, MaxLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 对应模块Id
        /// </summary>
        public long ModuleId { get; set; }

        public FunctionModule Module { get; set; }
    }
}
