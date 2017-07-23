using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class FunctionModuleActionDto : BaseDto
    {
        /// <summary>
        /// 操作码
        /// </summary>
        [Required, MaxLength(20), Display(Name = "操作码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20), Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 对应模块Id
        /// </summary>
        public long ModuleId { get; set; }
    }
}
