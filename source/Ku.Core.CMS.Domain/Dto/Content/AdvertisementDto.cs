//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AdvertisementDto.cs
// 功能描述：广告 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-10 21:27
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 广告
    /// </summary>
    public class AdvertisementDto : BaseProtectedDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(128)]
        [Display(Name = "名称", Prompt = "后台管理用，不会在网页显示")]
        public string Name { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(256)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(512)]
        [Display(Name = "图片")]
        public string ImageData { set; get; }

        /// <summary>
        /// Flash地址
        /// </summary>
        [StringLength(256)]
        [Display(Name = "Flash地址")]
        public string FlashUrl { set; get; }

        /// <summary>
        /// 链接
        /// </summary>
        [StringLength(512)]
        [Display(Name = "链接")]
        public string Link { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        [MaxLength(64)]
        [Display(Name = "来源")]
        public string Provenance { set; get; }

        /// 是否发布
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "是否发布", Prompt = "是|否")]
        public bool IsPublished { set; get; } = true;

        /// <summary>
        /// 点击数
        /// </summary>
        [Display(Name = "点击数")]
        public int Clicks { set; get; } = 0;

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        public virtual JsonUploadImage Image
        {
            get
            {
                return JsonUploadImage.Parse(ImageData).FirstOrDefault();
            }
        }
    }
}
