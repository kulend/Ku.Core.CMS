using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Core.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class UserDto : BaseDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "账号")]
        public string Account { set; get; }

        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, MaxLength(20)]
        [Display(Name = "密码")]
        public string Password { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "姓名")]
        [Required, MaxLength(20)]
        public string Name { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        [Display(Name = "手机号")]
        public string Mobile { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "状态", Prompt = "正常|禁用")]
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
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
