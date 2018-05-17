//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：UserDto.cs
// 功能描述：用户 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 10:45
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.UserCenter
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto : BaseProtectedDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Display(Name = "账号")]
        [Required, StringLength(20, MinimumLength = 5)]
        public string Account { set; get; }

        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, StringLength(64)]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required, StringLength(40)]
        [Display(Name = "昵称")]
        public string NickName { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "姓名")]
        public string RealName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        [Display(Name = "手机号")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { set; get; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        [Display(Name = "头像")]
        [DataType(DataType.ImageUrl)]
        public string HeadImage { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public EmSex Sex { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "状态", Prompt = "正常|禁用")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "备注")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        /// <summary>
        /// 是否是管理员
        /// </summary>
        [Display(Name = "管理员", Prompt = "是|否")]
        public bool IsAdmin { set; get; } = false;
    }
}
