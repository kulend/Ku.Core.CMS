using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vino.Core.Cache;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Data.Repository.Material;
using Vino.Core.CMS.Domain.Dto.Material;
using Vino.Core.CMS.Domain.Entity.Material;
using Vino.Core.EventBus;
using Vino.Core.Infrastructure.Exceptions;

namespace Vino.Core.CMS.Service.Material
{
    public partial class PictureService
    {
        private readonly IEventPublisher _eventPublisher;

        public PictureService(VinoDbContext context, ICacheService cache, IMapper mapper, IPictureRepository repository,
            IEventPublisher _eventPublisher)
            : this(context, cache, mapper, repository)
        {
            this._eventPublisher = _eventPublisher;
        }

        public async Task<(int count, List<PictureDto> items)> GetListAsync(int page, int rows)
        {
            var data = await _repository.PageQueryAsync(page, rows, null, "CreateTime desc");
            return (data.count, _mapper.Map<List<PictureDto>>(data.items));
        }

        public async Task AddAsync(PictureDto dto)
        {
            Picture model = _mapper.Map<Picture>(dto);
            model.IsDeleted = false;
            model.CreateTime = DateTime.Now;
            using (var trans = await _repository.BeginTransactionAsync())
            {
                await _repository.InsertAsync(model);
                await _repository.SaveAsync();

                //发送消息
                await _eventPublisher.PublishAsync("material_picture_upload", new PictureDto { Id = model.Id });
                trans.Commit();
            }
        }

        public async Task SaveAsync(PictureDto dto)
        {
            Picture model = _mapper.Map<Picture>(dto);
            //更新
            var entity = await _repository.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw new VinoDataNotFoundException("无法取得数据!");
            }

            entity.Title = model.Title;
            _repository.Update(entity);
            await _repository.SaveAsync();
        }
    }
}
