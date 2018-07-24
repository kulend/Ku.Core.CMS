//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：TimedTaskDto.cs
// 功能描述：定时任务 数据传输类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-22 08:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.System;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Dto.System
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class TimedTaskDto : BaseProtectedDto
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "分组")]
        public string Group { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        [Display(Name = "名称")]
        public string Name { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        public EmTimedTaskStatus Status { set; get; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [Display(Name = "Cron表达式", Description = "<a href=\"http://qqe2.com/cron/index\" target=\"_blank\">Cron表达式生成器</a>")]
        [Required, MaxLength(32)]
        public string Cron { get; set; }

        /// <summary>
        /// Assembly
        /// </summary>
        [Required, MaxLength(128)]
        [Display(Name = "Assembly")]
        public string AssemblyName { set; get; }

        /// <summary>
        /// Type
        /// </summary>
        [Required, MaxLength(128)]
        [Display(Name = "Type")]
        public string TypeName { set; get; }

        /// <summary>
        /// 开始运行时间
        /// </summary>
        [Display(Name = "开始运行时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? StarRunTime { get; set; }

        /// <summary>
        /// 结束运行时间
        /// </summary>
        [Display(Name = "结束运行时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? EndRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        [Display(Name = "下次运行时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 有效开始时间
        /// </summary>
        [Display(Name = "有效开始时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime ValidStartTime { set; get; }

        /// <summary>
        /// 有效结束时间
        /// </summary>
        [Display(Name = "有效结束时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [DataType(DataType.DateTime)]
        public DateTime ValidEndTime { set; get; }

        /// <summary>
        /// 运行次数
        /// </summary>
        [Display(Name = "运行次数")]
        public int RunTimes { set; get; }
    }
}
