//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：PictureService.cs
// 功能描述：图片素材 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.Material;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Domain.Entity.Material;
using Vino.Core.CMS.IService.Material;
using Vino.Core.EventBus;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Material
{
    public partial class PictureService : BaseService, IPictureService
    {
        protected readonly IPictureRepository _repository;
        private readonly IEventPublisher _eventPublisher;
		
        #region 构造函数
		
        public PictureService(
            IPictureRepository repository,
            IEventPublisher _eventPublisher)
        {
            this._repository = repository;
            this._eventPublisher = _eventPublisher;
        }

        #endregion

        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<PictureDto></returns>
        public async Task<List<PictureDto>> GetListAsync(PictureSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return Mapper.Map<List<PictureDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<PictureDto> items)> GetListAsync(int page, int size, PictureSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, Mapper.Map<List<PictureDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<PictureDto> GetByIdAsync(long id)
        {
            return Mapper.Map<PictureDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(PictureDto dto)
        {
            Picture model = Mapper.Map<Picture>(dto);
            var entity = await _repository.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw new VinoDataNotFoundException("ÎÞ·¨È¡µÃÊý¾Ý!");
            }

            entity.Title = model.Title;
            _repository.Update(entity);
            await _repository.SaveAsync();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task DeleteAsync(params long[] id)
        {
            if (await _repository.DeleteAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task RestoreAsync(params long[] id)
        {
            if (await _repository.RestoreAsync(id))
            {
                await _repository.SaveAsync();
            }
        }

        #endregion

        #region 其他方法
		
        public async Task AddAsync(PictureDto dto)
        {
            Picture model = Mapper.Map<Picture>(dto);
            model.IsDeleted = false;
            model.CreateTime = DateTime.Now;
            using (var trans = await _repository.BeginTransactionAsync())
            {
                await _repository.InsertAsync(model);
                await _repository.SaveAsync();

                await _eventPublisher.PublishAsync("material_picture_upload", new PictureDto { Id = model.Id });
                trans.Commit();
            }
        }
		
        #endregion
    }
}
