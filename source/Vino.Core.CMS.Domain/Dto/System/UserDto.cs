//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：UserDto.cs
// 功能描述：用户 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto.System
{
    public class UserDto : BaseDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(20), MinLength(5)]
        [Display(Name = "账号")]
        public string Account { set; get; }

        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, MaxLength(20), MinLength(6)]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [Required, MaxLength(20)]
        public string Name { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        [Display(Name = "手机号")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        [Display(Name = "头像")]
        [DataType(DataType.ImageUrl)]
        public string HeadImage { set; get; }

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
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
    }
}
