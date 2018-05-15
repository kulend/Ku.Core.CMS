//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：PictureService.cs
// 功能描述：图片素材 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.CMS.Domain.Dto.Material;
using Ku.Core.CMS.Domain.Entity.Material;
using Ku.Core.CMS.IService.Material;
using Ku.Core.EventBus;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Material
{
    public partial class PictureService : BaseService<Picture, PictureDto, PictureSearch>, IPictureService
    {
        private readonly IEventPublisher _eventPublisher;
		
        #region 构造函数
		
        public PictureService(
            IEventPublisher _eventPublisher)
        {
            this._eventPublisher = _eventPublisher;
        }

        #endregion

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(PictureDto dto)
        {
            using (var dapper = DapperFactory.Create())
            {
                var item = new
                {
                    dto.Title
                };
                await dapper.UpdateAsync<Picture>(item, new { dto.Id });
            }
        }

        public async Task AddAsync(PictureDto dto)
        {
            Picture model = Mapper.Map<Picture>(dto);
            model.Id = (dto.Id != 0) ? dto.Id : ID.NewID();
            model.IsDeleted = false;
            model.CreateTime = DateTime.Now;

            using (var dapper = DapperFactory.Create())
            {
                await dapper.InsertAsync(model);
                await _eventPublisher.PublishAsync("material_picture_upload", new PictureDto { Id = model.Id });
            }
        }
    }
}
