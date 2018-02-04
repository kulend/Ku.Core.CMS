//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
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
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Entity.Material
{
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

    public class PictureSearch : BaseSearch<Picture>
    {

    }
}
