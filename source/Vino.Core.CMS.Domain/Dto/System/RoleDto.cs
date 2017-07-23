using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class RoleDto : BaseDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20), DisplayName("名称")]
        public string Name { set; get; }

        /// <summary>
        /// 名称(英文)
        /// </summary>
        [Required, MaxLength(40), DisplayName("英文名")]
        public string NameEn { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "是否启用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
