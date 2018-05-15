//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：Picture.cs
// 功能描述：图片素材 实体类
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

namespace Ku.Core.CMS.Domain.Entity.Material
{
    /// <summary>
    /// 图片素材
    /// </summary>
    [Table("material_picture")]
    public class Picture : Material
    {
        [MaxLength(256)]
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

    }
}
