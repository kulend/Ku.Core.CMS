using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_user_role")]
    public class UserRole
    {
        public long UserId { set; get; }
        public virtual User User { set; get; }

        public long RoleId { set; get; }
        public virtual Role Role { set; get; }
    }
}
