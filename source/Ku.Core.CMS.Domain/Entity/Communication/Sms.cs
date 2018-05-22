//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Sms.cs
// 功能描述：短信 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-22 15:50
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Communication
{
    /// <summary>
    /// 短信
    /// </summary>
    [Table("communication_sms")]
    public class Sms : BaseProtectedEntity
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [StringLength(11)]
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [StringLength(256)]
        public string Content { set; get; }

        /// <summary>
        /// 短信数据
        /// </summary>
        [StringLength(512)]
        public string Data { set; get; }

        /// <summary>
        /// 短信模板ID
        /// </summary>
        public long SmsTempletId { set; get; }

        /// <summary>
        /// 短信模板
        /// </summary>
        public virtual SmsTemplet SmsTemplet { set; get; }
    }

    /// <summary>
    /// 短信 检索条件
    /// </summary>
    public class SmsSearch : BaseProtectedSearch<Sms>
    {

    }
}
