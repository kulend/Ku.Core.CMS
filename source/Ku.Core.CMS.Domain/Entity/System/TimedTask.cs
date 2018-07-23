//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：TimedTask.cs
// 功能描述：定时任务 实体类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-22 08:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Enum.System;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ku.Core.CMS.Domain.Entity.System
{
    /// <summary>
    /// 定时任务
    /// </summary>
    [Table("system_timed_task")]
    public class TimedTask : BaseProtectedEntity
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Required, MaxLength(64)]
        public string Group { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required, MaxLength(64)]
        public string Name { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public EmTimedTaskStatus Status { set; get; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [Required, MaxLength(32)]
        public string Cron { get; set; }

        [Required, MaxLength(128)]
        public string AssemblyName { set; get; }

        [Required, MaxLength(128)]
        public string TypeName { set; get; }

        /// <summary>
        /// 开始运行时间
        /// </summary>
        public DateTime? StarRunTime { get; set; }

        /// <summary>
        /// 结束运行时间
        /// </summary>
        public DateTime? EndRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 有效开始时间
        /// </summary>
        public DateTime ValidStartTime { set; get; }

        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime ValidEndTime { set; get; }
    }

    /// <summary>
    /// 定时任务 检索条件
    /// </summary>
    public class TimedTaskSearch : BaseProtectedSearch<TimedTask>
    {

    }
}
