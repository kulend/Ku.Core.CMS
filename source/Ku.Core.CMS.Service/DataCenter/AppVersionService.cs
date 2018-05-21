//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AppVersionService.cs
// 功能描述：应用版本 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-16 10:59
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.DataCenter
{
    public partial class AppVersionService : BaseService<AppVersion, AppVersionDto, AppVersionSearch>, IAppVersionService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AppVersionDto dto)
        {
            AppVersion model = Mapper.Map<AppVersion>(dto);
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
                    var item = new
                    {
                        //这里进行赋值
                        model.Version,
                        model.Content,
                        model.DownLoadUrl,
                    };
                    await dapper.UpdateAsync<AppVersion>(item, new { model.Id });
                }
            }
        }
    }
}
