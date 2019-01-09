//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：CommentService.cs
// 功能描述：评论 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 22:34
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
using Ku.Core.CMS.Domain.Entity.UserCenter;

namespace Ku.Core.CMS.Service.Content
{
    public partial class CommentService : BaseService<Comment, CommentDto, CommentSearch>, ICommentService
    {
        #region 构造函数

        public CommentService()
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
        public override async Task<(int count, List<CommentDto> items)> GetListAsync(int page, int size, CommentSearch search, dynamic sort)
        {
            using (var dapper = DapperFactory.Create())
            {
                var builder = new QueryBuilder().Select<Comment>().Concat<User>()
                    .From<Comment>("m")
                    .InnerJoin<User>("u").On(new ConditionBuilder().Equal<Comment, User>(m => m.UserId, u => u.Id))
                    .Where(search).Sort(sort as object).Limit(page, size);
                var data = await dapper.QueryPageAsync<Comment, User, Comment>(builder, (m, u) =>
                {
                    m.User = u;
                    return m;
                }, splitOn: "Id");

                return (data.count, Mapper.Map<List<CommentDto>>(data.items));
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public async Task SaveAsync(CommentDto dto)
        {
            Comment model = Mapper.Map<Comment>(dto);
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
                    await dapper.UpdateAsync<Comment>(item, new { model.Id });
                }
            }
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        public async Task AddAsync(CommentDto dto)
        {
            Comment model = Mapper.Map<Comment>(dto);
            //新增
            model.Id = ID.NewID();
            model.CreateTime = DateTime.Now;
            model.IsDeleted = false;
            model.Praises = 0;
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                await dapper.InsertAsync(model);

                var sql = $"update {dapper.Dialect.FormatTableName<Article>()} set {nameof(Article.Comments)}={nameof(Article.Comments)}+1 where Id=@Id";
                if (await dapper.ExecuteAsync(sql, new { Id = model.ArticleId }) == 1)
                {
                    dapper.Commit();
                }
            }
        }
    }
}
