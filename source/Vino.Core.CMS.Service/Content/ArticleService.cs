//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：ArticleService.cs
// 功能描述：文章 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Ku.Core.Extensions.Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vino.Core.CMS.Data.Repository.Content;
using Vino.Core.CMS.Domain;
using Vino.Core.CMS.Domain.Dto.Content;
using Vino.Core.CMS.Domain.Entity.Content;
using Vino.Core.CMS.IService.Content;
using Vino.Core.Infrastructure.Exceptions;
using Vino.Core.Infrastructure.Extensions;
using Vino.Core.Infrastructure.IdGenerator;

namespace Vino.Core.CMS.Service.Content
{
    public partial class ArticleService : BaseService, IArticleService
    {
        protected readonly IArticleRepository _repository;
        protected IDapper _dapper;

        #region 构造函数

        public ArticleService(IArticleRepository repository, IDapper dapper)
        {
            this._repository = repository;
            this._dapper = dapper;
        }

        #endregion

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
            return Mapper.Map<List<ArticleDto>>(data);
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
            return (data.count, Mapper.Map<List<ArticleDto>>(data.items));
        }

        /// <summary>
        /// 根据主键取得数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<ArticleDto> GetByIdAsync(long id)
        {
            return Mapper.Map<ArticleDto>(await this._repository.GetByIdAsync(id));
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(ArticleDto dto)
        {
            Article model = Mapper.Map<Article>(dto);
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
        public async Task DeleteAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.DeleteAsync<Article>(new DapperSql("Id IN (@Ids)", new { Ids = id }));
            }
        }

        /// <summary>
        /// 恢复数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task RestoreAsync(params long[] id)
        {
            using (var _dapper = DapperFactory.Create())
            {
                await _dapper.RestoreAsync<Article>(new DapperSql("Id IN (@Ids)", new { Ids = id }));
            }
        }

        #endregion

        #region 其他方法

        #endregion
    }
}
