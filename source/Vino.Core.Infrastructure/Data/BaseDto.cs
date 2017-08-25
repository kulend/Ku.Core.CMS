using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vino.Core.Infrastructure.Data
{
    public abstract partial class BaseDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataType("Hidden")]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { set; get; } = false;
    }
}
