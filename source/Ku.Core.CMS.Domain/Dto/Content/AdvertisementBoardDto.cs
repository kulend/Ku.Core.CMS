//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementBoardDto.cs
// 功能描述：广告牌 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 22:15
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 广告牌
    /// </summary>
    public class AdvertisementBoardDto : BaseProtectedDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(64)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 标记
        /// </summary>
        [Required, StringLength(32)]
        [Display(Name = "标记")]
        public string Tag { set; get; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用", Prompt = "是|否")]
        public bool IsEnable { set; get; } = true;

        public virtual IEnumerable<AdvertisementDto> Advertisements { set; get; }
    }
}
