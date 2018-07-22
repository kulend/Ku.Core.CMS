//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：TimedTaskService.cs
// 功能描述：定时任务 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-22 08:15
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using Ku.Core.CMS.IService.System;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.System
{
    public partial class TimedTaskService : BaseService<TimedTask, TimedTaskDto, TimedTaskSearch>, ITimedTaskService
    {
        #region 构造函数

        public TimedTaskService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(TimedTaskDto dto)
        {
            TimedTask model = Mapper.Map<TimedTask>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.CreateTime = DateTime.Now;
                model.IsDeleted = false;
                using (var dapper = DapperFactory.Create())
                {
                    await dapper.InsertAsync(model);
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.Create())
                {
                    var item = new {
                        //TODO:这里进行赋值
                    };
                    await dapper.UpdateAsync<TimedTask>(item, new { model.Id });
                }
            }
        }
    }
}
