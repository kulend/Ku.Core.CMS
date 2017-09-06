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
using Vino.Core.Infrastructure.IdGenerator;

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

        public async Task SaveAsync(PictureDto dto)
        {
            Picture model = _mapper.Map<Picture>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.IsDeleted = false;
                model.CreateTime = DateTime.Now;
                await _repository.InsertAsync(model);

                //发送消息
                await _eventPublisher.PublishAsync("material_picture_upload", new PictureDto { Id = model.Id });
            }
            else
            {
                //更新
                var entity = await _repository.GetByIdAsync(model.Id);
                if (entity == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据!");
                }

                entity.Title = model.Title;
                _repository.Update(entity);
            }
            await _repository.SaveAsync();
        }
    }
}
