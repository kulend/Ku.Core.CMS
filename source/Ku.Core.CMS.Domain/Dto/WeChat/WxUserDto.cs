//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：WxUserDto.cs
// 功能描述：微信用户 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Ku.Core.CMS.Domain.Enum;

namespace Ku.Core.CMS.Domain.Dto.WeChat
{
    public class WxUserDto : BaseProtectedDto
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccountDto Account { get; set; }

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
}
