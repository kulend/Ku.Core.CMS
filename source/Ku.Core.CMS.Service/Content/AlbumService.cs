//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：AlbumService.cs
// 功能描述：专辑 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-12-27 07:48
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Dnc.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Content
{
    public partial class AlbumService : BaseService<Album, AlbumDto, AlbumSearch>, IAlbumService
    {
        #region 构造函数

        public AlbumService()
        {
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(AlbumDto dto)
        {
            Album model = Mapper.Map<Album>(dto);
            if (model.IsPublished && !model.PublishedTime.HasValue)
            {
                model.PublishedTime = DateTime.Now;
            }
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
                        //这里进行赋值
                        model.Intro,
                        model.IsPublished,
                        model.OrderIndex,
                        model.PublishedTime,
                        model.Tags,
                        model.Title,
                        model.CoverData
                    };
                    await dapper.UpdateAsync<Album>(item, new { model.Id });
                }
            }
        }
    
        /// <summary>
        /// 增加点击数
        /// </summary>
        public async Task<bool> IncreaseVisitsAsync(long id, int count = 1)
        {
            using (var dapper = DapperFactory.Create())
            {
                var sql = $"update {dapper.Dialect.FormatTableName<Album>()} set {nameof(Album.Visits)}={nameof(Album.Visits)}+{count} where Id=@Id";
                return (await dapper.ExecuteAsync(sql, new { Id = id })) == 1;
            }
        }
    }
}
