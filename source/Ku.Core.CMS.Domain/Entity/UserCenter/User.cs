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

using Dnc.Extensions.Dapper.Attributes;
using Ku.Core.CMS.Domain.Enum;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
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

        /// <summary>
        /// 是否是管理员
        /// </summary>
        public bool IsAdmin { set; get; } = false;

        /// <summary>
        /// QQ
        /// </summary>
        [MaxLength(12)]
        public string QQ { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [MaxLength(256)]
        public string Signature { get; set; }

        /// <summary>
        /// 个性网站
        /// </summary>
        [MaxLength(256)]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// 密码加密
        /// </summary>
        public void EncryptPassword()
        {
            if (Password.IsNullOrEmpty())
            {
                return;
            }

            var result = CryptHelper.EncryptMD5(Password);
            if (this.Factor.HasValue)
            {
                result = CryptHelper.EncryptMD5(result + this.Factor.Value);
            }
            result = CryptHelper.EncryptSha256(result);
            Password = result;
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CheckPassword(string pwd)
        {
            if (pwd.IsNullOrEmpty())
            {
                return false;
            }

            var result = CryptHelper.EncryptMD5(pwd);
            if (this.Factor.HasValue)
            {
                result = CryptHelper.EncryptMD5(result + this.Factor.Value);
            }
            result = CryptHelper.EncryptSha256(result);

            return result.EqualOrdinalIgnoreCase(this.Password);
        }

        /// <summary>
        /// QQ登陆用户OpenID
        /// </summary>
        [StringLength(64)]
        public string QQOpenId { set; get; }
    }

    /// <summary>
    /// 用户 检索条件
    /// </summary>
    public class UserSearch : BaseProtectedSearch<User>
    {
        public bool? IsAdmin { set; get; }

        public string Mobile { set; get; }

        [Condition(WhenNull = WhenNull.Ignore)]
        public string QQOpenId { set; get; }
    }
}
