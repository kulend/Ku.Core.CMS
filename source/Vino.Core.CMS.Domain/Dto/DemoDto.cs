using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Vino.Core.CMS.Domain.Enum;
using Vino.Core.Infrastructure.Data;

namespace Vino.Core.CMS.Domain.Dto
{
    public class DemoDto : BaseDto
    {
        [MaxLength(30)]
        [Required]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        public EmSex Sex { set; get; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [DefaultValue(true)]
        [Display(Name = "状态", Prompt = "可用|禁用")]
        public bool IsEnable { set; get; } = true;

        /// <summary>
        /// 父ID
        /// </summary>
        [DataType("Hidden")]//页面隐藏
        public long? ParentId { get; set; }

        [Display(Name = "时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public new DateTime CreateTime { set; get; }

        [Display(Name = "日期")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
        [DataType(DataType.Date)]
        public DateTime CreateDate { set; get; }

        [Display(Name = "年份")]
        [DisplayFormat(DataFormatString = "yyyy年份")]
        [DataType("year")]
        public string Year { set; get; }

        [Display(Name = "备注")]
        public string Remark { set; get; }
    }
}
