using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ku.Core.CMS.Domain.Entity.System
{
    [Table("system_role_function")]
    public class RoleFunction
    {
        public long RoleId { set; get; }
        public virtual Role Role { set; get; }

        public long FunctionId { set; get; }
        public virtual Function Function { set; get; }
    }
}
