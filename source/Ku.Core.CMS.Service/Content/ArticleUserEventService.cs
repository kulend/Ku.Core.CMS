//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ArticleUserEventService.cs
// 功能描述：文章用户事件 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-09-14 22:48
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
using System.Collections.Generic;
using Dnc.Extensions.Dapper.Builders;

namespace Ku.Core.CMS.Service.Content
{
    public partial class ArticleUserEventService : BaseService<ArticleUserEvent, ArticleUserEventDto, ArticleUserEventSearch>, IArticleUserEventService
    {
        #region 构造函数

        public ArticleUserEventService()
        {
        }

        #endregion

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="size">条数</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <returns>count：条数；items：分页数据</returns>
        public override async Task<(int count, List<ArticleUserEventDto> items)> GetListAsync(int page, int size, ArticleUserEventSearch search, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<ArticleUserEvent>().Concat<Article>()
                    .From<ArticleUserEvent>("m")
                    .InnerJoin<Article>("a").On(new ConditionBuilder().Equal<ArticleUserEvent, Article>(m => m.ArticleId, a => a.Id))
                    .Where(search).Sort(sort as object).Limit(page, size);
                var data = await dapper.QueryPageAsync<ArticleUserEvent, Article, ArticleUserEvent>(builder, (m, a) =>
                {
                    m.Article = a;
                    return m;
                }, splitOn: "Id");

                return (data.count, Mapper.Map<List<ArticleUserEventDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(ArticleUserEventDto dto)
        {
            ArticleUserEvent model = Mapper.Map<ArticleUserEvent>(dto);
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
                        //TODO:这里进行赋值
                    };
                    await dapper.UpdateAsync<ArticleUserEvent>(item, new { model.Id });
                }
            }
        }


    }
}
