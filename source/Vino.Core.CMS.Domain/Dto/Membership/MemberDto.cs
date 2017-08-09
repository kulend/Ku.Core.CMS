using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Enum;

namespace Vino.Core.CMS.Domain.Dto.Membership
{
    public class MemberDto : BaseDto
    {
        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "密码")]
        public string Password { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(40)]
        [Display(Name = "昵称")]
        public string Name { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        [Display(Name = "手机号")]
        public string Mobile { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public EmSex Sex { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "状态", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime? LastLoginTime { set; get; }

        /// <summary>
        /// 最后登陆IP
        /// </summary>
        [MaxLength(40)]
        public string LastLoginIp { set; get; }

        /// <summary>
        /// 积分
        /// </summary>
        [Display(Name="积分")]
        public int Points { set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        [Display(Name = "等级")]
        public int Level { set; get; }

        /// <summary>
        /// 会员类型ID
        /// </summary>
        [Display(Name = "会员类型")]
        public long? MemberTypeId { set; get; }

        public MemberTypeDto MemberType { set; get; }
    }
}
