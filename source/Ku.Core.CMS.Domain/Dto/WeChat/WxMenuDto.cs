//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：WxMenuDto.cs
// 功能描述：微信菜单 数据传输类
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

namespace Ku.Core.CMS.Domain.Dto.WeChat
{
    /// <summary>
    /// 微信菜单
    /// </summary>
    public class WxMenuDto : BaseProtectedDto
    {
        /// <summary>
        /// 公众号ID
        /// </summary>
        [DataType("Hidden")]
        public long AccountId { get; set; }

        public WxAccountDto Account { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(40)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 菜单数据
        /// </summary>
        [Display(Name = "菜单数据")]
        [DataType(DataType.MultilineText)]
        public string MenuData { set; get; }

        /// <summary>
        /// 最后发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? PublishTime { set; get; }

        /// <summary>
        /// 是否个性化菜单
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "个性化菜单", Prompt = "是|否")]
        public bool IsIndividuation { set; get; } = false;

        /// <summary>
        /// 微信菜单ID
        /// </summary>
        [MaxLength(32)]
        [Display(Name = "微信菜单ID")]
        public string WxMenuId { set; get; }

        #region MatchRule

        [MaxLength(20)]
        [Display(Name = "用户标签ID")]
        public string MatchRuleTagId { set; get; }

        [MaxLength(1)]
        [Display(Name = "性别")]
        public string MatchRuleSex { set; get; }

        [MaxLength(1)]
        [Display(Name = "客户端")]
        public string MatchRuleClient { set; get; }

        [MaxLength(40)]
        [Display(Name = "国家")]
        public string MatchRuleCountry { set; get; }

        [MaxLength(40)]
        [Display(Name = "省份")]
        public string MatchRuleProvince { set; get; }

        [MaxLength(40)]
        [Display(Name = "城市")]
        public string MatchRuleCity { set; get; }

        [MaxLength(10)]
        [Display(Name = "语言")]
        public string MatchRuleLanguage { set; get; }

        #endregion
    }
}
