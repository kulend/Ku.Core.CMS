using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.Material;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Domain.Entity.Material;
using Vino.Core.CMS.IService.Material;
using Vino.Core.EventBus;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Material
{
    public partial class PictureService : IPictureService
    {
        private readonly IEventPublisher _eventPublisher;

        public PictureService(VinoDbContext context, ICacheService cache, IMapper mapper, IPictureRepository repository,
            IEventPublisher _eventPublisher)
            : this(context, cache, mapper, repository)
        {
            this._eventPublisher = _eventPublisher;
        }

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
            return _mapper.Map<List<PictureDto>>(data);
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
            return (data.count, _mapper.Map<List<PictureDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<PictureDto> GetByIdAsync(long id)
        {
            return _mapper.Map<PictureDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(PictureDto dto)
        {
            Picture model = _mapper.Map<Picture>(dto);
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
        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }

        #endregion

        #region 其他方法
		
        public async Task AddAsync(PictureDto dto)
        {
            Picture model = _mapper.Map<Picture>(dto);
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
