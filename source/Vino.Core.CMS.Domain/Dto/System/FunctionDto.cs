using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Dto.System;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class FunctionDto : BaseDto
    {
        /// <summary>
        /// 父功能
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name= "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "权限码")]
        public string AuthCode { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        public bool IsEnable { set; get; }

        /// <summary>
        /// 是否有子功能
        /// </summary>
        public bool HasSub { set; get; } = false;

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 角色是否有该功能权限
        /// </summary>
        public bool IsRoleHasAuth { set; get; } = false;
    }
}
