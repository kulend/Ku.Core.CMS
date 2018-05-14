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
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.IService.Content;
using Ku.Core.Extensions.Dapper;
using Ku.Core.Infrastructure.Exceptions;
using Ku.Core.Infrastructure.Extensions;
using Ku.Core.Infrastructure.IdGenerator;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Ku.Core.CMS.Service.Content
{
    public partial class ArticleService : BaseService<Article, ArticleDto, ArticleSearch>, IArticleService
    {
        #region 构造函数

        public ArticleService()
        {
        }

        #endregion

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
                    var entity = await dapper.QueryOneAsync<Article>(new { model.Id });
                    if (entity == null)
                    {
                        throw new VinoDataNotFoundException("无法取得相关数据！");
                    }

                    dynamic item = new ExpandoObject();
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
                    if (model.IsPublished && !entity.PublishedTime.HasValue)
                    {
                        item.PublishedTime = DateTime.Now;
                    }
                    await dapper.UpdateAsync<Article>(item, new { model.Id });
                }
            }
        }
    }
}
