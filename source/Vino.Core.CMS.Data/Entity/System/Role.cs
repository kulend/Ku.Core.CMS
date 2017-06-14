using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Common;

namespace Vino.Core.CMS.Data.Entity.System
{
    [Table("system_role")]
    public class Role : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(40)]
        public string Name { set; get; }

        //[ForeignKey("CreateUser")]
        //public long CreateUserId { set; get; }

        //public virtual User CreateUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<RoleMenu> RoleMenus { get; set; }
    }
}
