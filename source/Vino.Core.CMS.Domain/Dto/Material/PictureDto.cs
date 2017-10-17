using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.EventBus;

namespace Vino.Core.CMS.Domain.Dto.Material
{
    public class PictureDto : MaterialDto
    {
        public string ThumbPath { set; get; }

        /// <summary>
        /// 是否已压缩
        /// </summary>
        public bool IsCompressed { set; get; }

        public int TryCompressCount { set; get; } = 0;
    }
}
