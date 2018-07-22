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

        [Display(Name = "状态")]
        public short Status { set; get; }

        [Display(Name = "Cron表达式")]
        public string Cron { get; set; }

        [Display(Name = "开始运行时间")]
        public DateTime? StarRunTime { get; set; }

        [Display(Name = "结束运行时间")]
        public DateTime? EndRunTime { get; set; }

        [Display(Name = "下次运行时间")]
        public DateTime? NextRunTime { get; set; }

        public DateTime ValidStartTime { set; get; }

        public DateTime ExpireTime { set; get; }
    }
}
