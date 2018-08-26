//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Picture.cs
// 功能描述：图片素材 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-28 14:27
//
//----------------------------------------------------------------

using Dnc.Extensions.Dapper.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.MaterialCenter
{
    /// <summary>
    /// 图片素材
    /// </summary>
    [Table("materialcenter_picture")]
    public class Picture : BaseMaterial
    {
        [StringLength(256)]
        public string ThumbPath { set; get; }

        /// <summary>
        /// 是否已压缩
        /// </summary>
        public bool IsCompressed { set; get; }
    }

    /// <summary>
    /// 图片素材 检索条件
    /// </summary>
    public class PictureSearch : BaseProtectedSearch<Picture>
    {
        [Condition(WhenNull = WhenNull.Ignore, Operation = ConditionOperation.Custom, 
            CustomSql = "EXISTS (SELECT * FROM materialcenter_user_material_group_ref ref WHERE ref.MaterialId=m.Id AND ref.GroupId=@value)")]
        public long? GroupId { set; get; }
    }
}
