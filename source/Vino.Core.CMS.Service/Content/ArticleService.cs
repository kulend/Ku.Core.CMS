using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Domain.Dto.Content;
using Vino.Core.CMS.Domain.Entity.Content;
using Vino.Core.CMS.IService.Content;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Content
{
    public partial class ArticleService : IArticleService
    {
        #region 自动生成的方法

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>List<ArticleDto></returns>
        public async Task<List<ArticleDto>> GetListAsync(ArticleSearch where, string sort)
        {
            var data = await _repository.QueryAsync(where.GetExpression(), sort ?? "CreateTime desc");
            return _mapper.Map<List<ArticleDto>>(data);
        }

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public async Task<(int count, List<ArticleDto> items)> GetListAsync(int page, int size, ArticleSearch where, string sort)
        {
            var data = await _repository.PageQueryAsync(page, size, where.GetExpression(), sort ?? "CreateTime desc");
            return (data.count, _mapper.Map<List<ArticleDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<ArticleDto> GetByIdAsync(long id)
        {
            return _mapper.Map<ArticleDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
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
                    throw new VinoDataNotFoundException("无法取得文章数据！");
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

        #endregion
    }
}
