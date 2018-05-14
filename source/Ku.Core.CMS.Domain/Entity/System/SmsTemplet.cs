//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：SmsTemplet.cs
// 功能描述：短信模板 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-02 09:50
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.System
{
    /// <summary>
    /// 短信模板
    /// </summary>
    [Table("system_sms_templet")]
    public class SmsTemplet : BaseProtectedEntity
    {
        /// <summary>
        /// 标记
        /// </summary>
        [Required, StringLength(64)]
        public string Tag { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(100)]
        public string Name { set; get; }

        /// <summary>
        /// 示例
        /// </summary>
        [StringLength(400)]
        public string Example { get; set; }

        /// <summary>
        /// 模板
        /// </summary>
        [StringLength(400)]
        public string Templet { get; set; }

        /// <summary>
        /// 模板KEY
        /// </summary>
        [StringLength(64)]
        public string TempletKey { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [StringLength(40)]
        public string Signature { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 过期时间（分钟）
        /// </summary>
        public int ExpireMinute { set; get; }

        /// <summary>
        /// 短信账号ID
        /// </summary>
        public long? SmsAccountId { set; get; }

        /// <summary>
        /// 短信账号
        /// </summary>
        public virtual SmsAccount SmsAccount { set; get; }
    }

    /// <summary>
    /// 短信模板 检索条件
    /// </summary>
    public class SmsTempletSearch : BaseProtectedSearch<SmsTemplet>
    {

    }
}
