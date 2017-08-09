using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Entity.Membership
{
    [Table("membership_member_type")]
    public class MemberType : BaseProtectedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { set; get; }
    }
}
