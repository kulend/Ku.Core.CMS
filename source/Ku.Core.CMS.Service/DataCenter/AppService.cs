//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AppService.cs
// 功能描述：应用 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using Ku.Core.CMS.IService.DataCenter;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.Helper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.DataCenter
{
    public partial class AppService : BaseService<App, AppDto, AppSearch>, IAppService
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AppDto dto)
        {
            App model = Mapper.Map<App>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.AppKey = CryptHelper.EncryptMD5Short(model.Id.ToString());
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
                        model.Type,
                        model.Name,
                        model.Intro,
                        model.IconData,
                        model.DownloadUrl,
                    };
                    await dapper.UpdateAsync<App>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 根据Appkey取得应用信息
        /// </summary>
        public async Task<AppDto> GetByAppkeyAsync(string appkey)
        {
            if (appkey.IsNullOrEmpty()) return null;
            using (var dapper = DapperFactory.Create())
            {
                var model = await dapper.QueryOneAsync<App>(new { AppKey = appkey, IsDeleted = false});
                return Mapper.Map<AppDto>(model);
            }
        }
    }
}
