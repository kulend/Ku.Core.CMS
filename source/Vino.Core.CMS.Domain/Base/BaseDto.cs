using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ku.Core.CMS.Domain
{
    public abstract class BaseDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataType("Hidden")]
        public long Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime CreateTime { set; get; }
    }

    public abstract class BaseProtectedDto : BaseDto
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        [DefaultValue(false)]
        [Display(Name = "是否删除", Prompt = "是|否")]
        public bool IsDeleted { set; get; } = false;
    }
}
