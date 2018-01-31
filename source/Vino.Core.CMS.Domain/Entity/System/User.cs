using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Vino.Core.Infrastructure.Data;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.Helper;

namespace Vino.Core.CMS.Domain.Entity.System
{
    [Table("system_user")]
    public class User : BaseProtectedEntity
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required, MaxLength(20)]
        public string Account { set; get; }

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
        /// 用户角色集合
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }

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
		
    }
	
    public class UserSearch : BaseSearch<User>
    {

    }
}
