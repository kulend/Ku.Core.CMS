//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ArticleService.cs
// 功能描述：文章 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using AutoMapper;
using Dnc.Extensions.Dapper;
using Ku.Core.Cache;
using Ku.Core.CMS.Domain;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using Ku.Core.CMS.Domain.Enum.Content;
using Ku.Core.CMS.IService.Content;
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
        private readonly ICacheProvider _cache;

        #region 构造函数

        public ArticleService(ICacheProvider cache)
        {
            _cache = cache;
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
                model.Praises = 0;
                model.Collects = 0;
                model.Comments = 0;
                model.Tags = "|" + string.Join("|", model.Tags.SplitRemoveEmpty(',')) + "|";
                if (model.IsPublished && !model.PublishedTime.HasValue)
                {
                    model.PublishedTime = DateTime.Now;
                }
                model.IsDeleted = false;

                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    await dapper.InsertAsync(model);

                    if (model.AlbumId.HasValue)
                    {
                        var cnt = await dapper.QueryCountAsync<Article>(new { AlbumId = model.AlbumId.Value, IsPublished = true});
                        await dapper.UpdateAsync<Album>(new { Videos = cnt }, new { id = model.AlbumId.Value });
                    }

                    dapper.Commit();
                }
            }
            else
            {
                //更新
                using (var dapper = DapperFactory.CreateWithTrans())
                {
                    var entity = await dapper.QueryOneAsync<Article>(new { model.Id });
                    if (entity == null)
                    {
                        throw new KuDataNotFoundException("无法取得相关数据！");
                    }

                    dynamic item = new ExpandoObject();
                    item.Title = model.Title;
                    item.Author = model.Author;
                    item.Provenance = model.Provenance;
                    item.CoverData = model.CoverData;
                    item.OrderIndex = model.OrderIndex;
                    item.Keyword = model.Keyword.R("，", ",");
                    item.SubTitle = model.SubTitle;
                    item.IsPublished = model.IsPublished;
                    item.Content = model.Content;
                    item.ContentType = model.ContentType;
                    item.Intro = model.Intro;
                    item.Duration = model.Duration;
                    item.Tags = "|" + string.Join("|", model.Tags.SplitRemoveEmpty(',')) + "|";
                    if (model.IsPublished && !model.PublishedTime.HasValue)
                    {
                        model.PublishedTime = DateTime.Now;
                    }
                    item.PublishedTime = model.PublishedTime;

                    await dapper.UpdateAsync<Article>(item, new { model.Id });

                    if (entity.AlbumId.HasValue)
                    {
                        var cnt = await dapper.QueryCountAsync<Article>(new { AlbumId = entity.AlbumId.Value, IsPublished = true });
                        await dapper.UpdateAsync<Album>(new { Videos = cnt }, new { id = entity.AlbumId.Value });
                    }

                    dapper.Commit();
                }
            }

            //清除缓存
        }

        /// <summary>
        /// 增加点击数
        /// </summary>
        public async Task<bool> IncreaseVisitsAsync(long id, int count = 1)
        {
            using (var dapper = DapperFactory.Create())
            {
                var sql = $"update {dapper.Dialect.FormatTableName<Article>()} set {nameof(Article.Visits)}={nameof(Article.Visits)}+{count} where Id=@Id";
                return (await dapper.ExecuteAsync(sql, new { Id = id })) == 1;
            }
        }

        /// <summary>
        /// 点赞
        /// </summary>
        public async Task<bool> PraiseAsync(long id, long userId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                var @event = await dapper.QueryOneAsync<ArticleUserEvent>(new { ArticleId = id , UserId = userId, Event = EmArticleUserEvent.Praise });
                if (@event == null)
                {
                    @event = new ArticleUserEvent
                    {
                        Id = ID.NewID(),
                        ArticleId = id,
                        UserId = userId,
                        Event = Domain.Enum.Content.EmArticleUserEvent.Praise,
                        IsCancel = false,
                        EventTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        CancelTime = null,
                        IsDeleted = false
                    };
                    await dapper.InsertAsync<ArticleUserEvent>(@event);
                }
                else
                {
                    if (!@event.IsDeleted && !@event.IsCancel)
                    {
                        throw new KuException("你已点过赞了，无需重复点赞！");
                    }

                    await dapper.UpdateAsync<ArticleUserEvent>(new {
                        IsCancel = false,
                        IsDeleted = false,
                        EventTime = DateTime.Now,
                        CancelTime = (DateTime?)null,
                    }, new { @event.Id });
                }

                var sql = $"update {dapper.Dialect.FormatTableName<Article>()} set {nameof(Article.Praises)}={nameof(Article.Praises)}+1 where Id=@Id";
                if(await dapper.ExecuteAsync(sql, new { Id = id }) == 1)
                {
                    dapper.Commit();
                }
            }
            return true;
        }

        /// <summary>
        /// 检查是否点赞
        /// </summary>
        public async Task<bool> HasUserPraisedAsync(long id, long userId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var count = await dapper.QueryCountAsync<ArticleUserEvent>(new { ArticleId = id, UserId = userId, Event = EmArticleUserEvent.Praise, IsCancel = false, IsDeleted = false });
                return count > 0;
            }
        }

        /// <summary>
        /// 收藏
        /// </summary>
        public async Task<bool> CollectAsync(long id, long userId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                var @event = await dapper.QueryOneAsync<ArticleUserEvent>(new { ArticleId = id, UserId = userId, Event = EmArticleUserEvent.Collect });
                if (@event == null)
                {
                    @event = new ArticleUserEvent
                    {
                        Id = ID.NewID(),
                        ArticleId = id,
                        UserId = userId,
                        Event = Domain.Enum.Content.EmArticleUserEvent.Collect,
                        IsCancel = false,
                        EventTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        CancelTime = null,
                        IsDeleted = false
                    };
                    await dapper.InsertAsync<ArticleUserEvent>(@event);
                }
                else
                {
                    if (!@event.IsDeleted && !@event.IsCancel)
                    {
                        throw new KuException("你已收藏成功！");
                    }

                    await dapper.UpdateAsync<ArticleUserEvent>(new
                    {
                        IsCancel = false,
                        IsDeleted = false,
                        EventTime = DateTime.Now,
                        CancelTime = (DateTime?)null,
                    }, new { @event.Id });
                }

                var sql = $"update {dapper.Dialect.FormatTableName<Article>()} set {nameof(Article.Collects)}={nameof(Article.Collects)}+1 where Id=@Id";
                if (await dapper.ExecuteAsync(sql, new { Id = id }) == 1)
                {
                    dapper.Commit();
                }
            }
            return true;
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        public async Task<bool> UnCollectAsync(long id, long userId)
        {
            using (var dapper = DapperFactory.CreateWithTrans())
            {
                var @event = await dapper.QueryOneAsync<ArticleUserEvent>(new { ArticleId = id, UserId = userId, Event = EmArticleUserEvent.Collect });
                if (@event == null)
                {
                    return true;
                }
                else
                {
                    if (@event.IsCancel)
                    {
                        throw new KuException("你已取消收藏！");
                    }

                    await dapper.UpdateAsync<ArticleUserEvent>(new
                    {
                        IsCancel = true,
                        IsDeleted = false,
                        EventTime = DateTime.Now,
                        CancelTime = DateTime.Now,
                    }, new { @event.Id });
                }

                var sql = $"update {dapper.Dialect.FormatTableName<Article>()} set {nameof(Article.Collects)}={nameof(Article.Collects)}-1 where Id=@Id";
                if (await dapper.ExecuteAsync(sql, new { Id = id }) == 1)
                {
                    dapper.Commit();
                }
            }
            return true;
        }

        /// <summary>
        /// 检查是否收藏
        /// </summary>
        public async Task<bool> HasUserCollectedAsync(long id, long userId)
        {
            using (var dapper = DapperFactory.Create())
            {
                var count = await dapper.QueryCountAsync<ArticleUserEvent>(new { ArticleId = id, UserId = userId, Event = EmArticleUserEvent.Collect, IsCancel = false, IsDeleted = false });
                return count > 0;
            }
        }
    }
}
