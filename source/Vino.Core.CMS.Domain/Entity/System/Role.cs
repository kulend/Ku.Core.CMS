using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_role")]
    public class Role : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 名称(英文)
        /// </summary>
        [Required, MaxLength(40)]
        [Display(Name = "英文名")]
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

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户功能权限集合
        /// </summary>
        public virtual ICollection<RoleFunction> RoleFunctions { get; set; }
    }

    public class RoleSearch : BaseSearch<Role>
    {

    }
}
