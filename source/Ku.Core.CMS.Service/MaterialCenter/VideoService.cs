//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：VideoService.cs
// 功能描述：视频素材 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 23:14
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.MaterialCenter;
using Ku.Core.CMS.Domain.Entity.MaterialCenter;
using Ku.Core.CMS.IService.MaterialCenter;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.MaterialCenter
{
    public partial class VideoService : BaseService<Video, VideoDto, VideoSearch>, IVideoService
    {
        #region 构造函数

        public VideoService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(VideoDto dto)
        {
            Video model = Mapper.Map<Video>(dto);
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
                    await dapper.UpdateAsync<Video>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        public async Task AddAsync(VideoDto dto)
        {
            Video model = Mapper.Map<Video>(dto);
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;
            using (var dapper = DapperFactory.Create())
            {
                await dapper.InsertAsync(model);
            }
        }
    }
}
