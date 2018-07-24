//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ITimedTaskService.cs
// 功能描述：定时任务 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-22 08:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface ITimedTaskService : IBaseService<TimedTask, TimedTaskDto, TimedTaskSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(TimedTaskDto dto);

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateTaskStatusAsync(long id, dynamic data);

        Task IncreaseRunTimesAsync(long id);
    }
}
