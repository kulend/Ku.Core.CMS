//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：User.cs
// 功能描述：用户 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-16 10:45
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.UserCenter
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("usercenter_user")]
    public class User : BaseProtectedEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required, StringLength(20)]
        public string Account { set; get; }

        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, StringLength(64)]
        public string Password { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required, StringLength(40)]
        public string NickName { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(20)]
        public string RealName { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        public string Mobile { set; get; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [StringLength(256)]
        public string Email { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        [StringLength(256)]
        public string HeadImage { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public EmSex Sex { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remarks { get; set; }

        /// <summary>
        /// 随机因子
        /// </summary>
        public int? Factor { set; get; }
    }

    /// <summary>
    /// 用户 检索条件
    /// </summary>
    public class UserSearch : BaseProtectedSearch<User>
    {

    }
}
