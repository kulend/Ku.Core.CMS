//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：Member.cs
// 功能描述：会员 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.CMS.Domain.Enum;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;

namespace Vino.Core.CMS.Domain.Entity.Membership
{
    [Table("membership_member")]
    public class Member : BaseProtectedEntity
    {
        /// <summary>
        /// 账号密码
        /// </summary>
        [Required, MaxLength(64)]
        public string Password { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(40)]
        public string Name { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string Mobile { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public EmSex Sex { set; get; }

        /// <summary>
        /// 随机因子
        /// </summary>
        public int? Factor { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
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
        /// 积分
        /// </summary>
        public int Points { set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 会员类型ID
        /// </summary>
        public long? MemberTypeId { set; get; }

        public MemberType MemberType { set; get; }
    }
	
	public class MemberSearch : BaseProtectedSearch<Member>
    {

    }
}
