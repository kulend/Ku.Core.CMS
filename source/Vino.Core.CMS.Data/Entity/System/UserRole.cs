using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Common;

namespace Vino.Core.CMS.Data.Entity.System
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
