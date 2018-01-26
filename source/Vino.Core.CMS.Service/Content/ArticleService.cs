using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Content;
using Vino.Core.CMS.Domain.Dto.Content;
using Vino.Core.CMS.Domain.Entity.Content;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Content
{
    public partial class ArticleService
    {
        public async Task<(int count, List<ArticleDto> items)> GetListAsync(int page, int size)
        {
            var data = await _repository.PageQueryAsync(page, size, null, "CreateTime desc");
            return (data.count, _mapper.Map<List<ArticleDto>>(data.items));
        }

        public async Task<ArticleDto> GetByIdAsync(long id)
        {
            return _mapper.Map<ArticleDto>(await this._repository.GetByIdAsync(id));
        }

        public async Task SaveAsync(ArticleDto dto)
        {
            Article model = _mapper.Map<Article>(dto);
            if (model.Id == 0)
            {
                //新增
                model.Id = ID.NewID();
                model.Keyword = model.Keyword.R("，", ",");
                model.CreateTime = DateTime.Now;
                model.Visits = 0;
                if (model.IsPublished && !model.PublishedTime.HasValue)
                {
                    model.PublishedTime = DateTime.Now;
                }
                model.IsDeleted = false;
                await _repository.InsertAsync(model);
            }
            else
            {
                //更新
                var item = await _repository.GetByIdAsync(model.Id);
                if (item == null)
                {
                    throw new VinoDataNotFoundException("无法取得数据！");
                }

                item.Title = model.Title;
                item.Author = model.Author;
                item.Provenance = model.Provenance;
                item.OrderIndex = model.OrderIndex;
                item.Keyword = model.Keyword.R("，", ",");
                item.SubTitle = model.SubTitle;
                item.IsPublished = model.IsPublished;
                item.PublishedTime = model.PublishedTime;
                item.Content = model.Content;
                item.ContentType = model.ContentType;
                if (item.IsPublished && !item.PublishedTime.HasValue)
                {
                    item.PublishedTime = DateTime.Now;
                }
                _repository.Update(item);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
        }
    }
}
