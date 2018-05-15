//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Column.cs
// 功能描述：栏目 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-23 14:15
//
//----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.Content
{
    /// <summary>
    /// 栏目
    /// </summary>
    [Table("content_column")]
    public class Column : BaseProtectedEntity
    {
        /// <summary>
        /// 父栏目
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, StringLength(100)]
        public string Name { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(128)]
        public string Title { set; get; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderIndex { set; get; }

    }

    /// <summary>
    /// 栏目 检索条件
    /// </summary>
    public class ColumnSearch : BaseProtectedSearch<Column>
    {
        /// <summary>
        /// 父栏目
        /// </summary>
        [SearchCondition(ignoreWhenNull: false)]
        public long? ParentId { get; set; }
    }
}
