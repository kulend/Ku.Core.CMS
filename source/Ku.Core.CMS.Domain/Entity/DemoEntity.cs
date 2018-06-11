using Ku.Core.CMS.Domain.Enum.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ku.Core.CMS.Domain.Entity
{
    [Table("demo1")]
    public class DemoEntity : BaseProtectedEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Required, MaxLength(256)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 详情模式
        /// </summary>
        [Display(Name = "详情模式")]
        public EmArticleContentType Type { set; get; }

        /// <summary>
        /// 详情内容
        /// </summary>
        [Display(Name = "详情内容")]
        public string Content { set; get; }

        public int? IntValue { set; get; }

        public short ShortValue { set; get; }

        public decimal? DecimalValue { set; get; }

        public float FloatValue { set; get; }

        public bool BoolValue { set; get; }

        public double DoubleValue { set; get; }

    }
}
