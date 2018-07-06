//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MaskWord.cs
// 功能描述：屏蔽关键词 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-03 14:22
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.Content;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 屏蔽关键词
    /// </summary>
    [Table("content_maskword")]
    public class MaskWord : BaseEntity
    {
        /// <summary>
        /// 等级
        /// </summary>
        public EmMaskWordLevel Level { set; get; }

        /// <summary>
        /// 关键词
        /// </summary>
        [Required, StringLength(64)]
        public string Word { set; get; }

        /// <summary>
        /// 标签
        /// </summary>
        [StringLength(128)]
        public string Tag { set; get; }

        /// <summary>
        /// 匹配次数
        /// </summary>
        public int MatchCount { set; get; }
    }

    /// <summary>
    /// 屏蔽关键词 检索条件
    /// </summary>
    public class MaskWordSearch : BaseSearch<MaskWord>
    {
        /// <summary>
        /// 等级
        /// </summary>
        public EmMaskWordLevel? Level { set; get; }
    }
}
