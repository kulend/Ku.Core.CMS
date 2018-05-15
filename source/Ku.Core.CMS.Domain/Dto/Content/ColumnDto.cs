//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ColumnDto.cs
// 功能描述：栏目 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-23 14:15
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.Content
{
    /// <summary>
    /// 栏目
    /// </summary>
    public class ColumnDto : BaseProtectedDto
    {
        /// <summary>
        /// 父栏目
        /// </summary>
        [DataType("Hidden")]
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(100)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(128)]
        [Display(Name = "标题")]
        public string Title { set; get; }

        /// <summary>
        /// 排序值
        /// </summary>
        [Display(Name = "排序值")]
        public int OrderIndex { set; get; }
    }
}
