//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxUser.cs
// 功能描述：微信用户 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Ku.Core.CMS.Domain.Enum;
using Ku.Core.Infrastructure.Attributes;

namespace Ku.Core.CMS.Domain.Entity.WeChat
{
    [Table("wechat_user")]
    public class WxUser : BaseProtectedEntity
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccount Account { get; set; }

        [MaxLength(64), Required]
        [Display(Name = "OpenId")]
        public string OpenId { set; get; }

        [MaxLength(64)]
        [Display(Name = "UnionId")]
        public string UnionId { set; get; }

        [Display(Name = "是否已关注")]
        public bool IsSubscribe { set; get; }

        [MaxLength(100)]
        [Display(Name = "昵称")]
        public string NickName { set; get; }

        [MaxLength(500)]
        [Display(Name = "头像")]
        public string HeadImgUrl { set; get; }

        [Display(Name = "性别")]
        public EmSex Sex { set; get; }

        [MaxLength(100)]
        [Display(Name = "国家")]
        public string Country { set; get; }

        [MaxLength(100)]
        [Display(Name = "省份")]
        public string Province { set; get; }

        [MaxLength(100)]
        [Display(Name = "城市")]
        public string City { set; get; }

        [MaxLength(20)]
        [Display(Name = "语言")]
        public string Language { set; get; }

        [MaxLength(30)]
        [Display(Name = "备注名")]
        public string Remark { set; get; }

        /// <summary>
        /// 关注时间
        /// </summary>
        [Display(Name = "关注时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? SubscribeTime { set; get; }

        /// <summary>
        /// 用户标签
        /// </summary>
        [MaxLength(256)]
        [Display(Name = "用户标签")]
        public string UserTags { get; set; }
    }

    public class WxUserSearch : BaseProtectedSearch<WxUser>
    {
        public long AccountId { set; get; }

        [SearchCondition(SearchConditionOperation.Contains)]
        public string NickName { set; get; }
    }
}
