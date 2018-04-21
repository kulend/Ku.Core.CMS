using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Enum.Membership;

namespace Vino.Core.CMS.Web.Backend.Areas.Membership.Views.MemberPoint
{
    public class MemberPointAdjustDto
    {
        /// <summary>
        /// 积分类型
        /// </summary>
        [Display(Name = "积分类型")]
        public EmMemberPointType Type { set; get; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public long[] MemberId { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name = "变动积分", Description = "输入负值则进行积分扣除")]
        [Range(-9999, 9999)]
        public int Points { get; set; }

        /// <summary>
        /// 业务备注
        /// </summary>
        [StringLength(200)]
        [Display(Name = "业务备注", Prompt = "请备注此次积分变动的原因")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }
    }
}
