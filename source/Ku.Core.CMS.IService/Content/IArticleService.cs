//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IArticleService.cs
// 功能描述：文章 业务逻辑接口类
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Dnc.Extensions.Dapper.Builders;
using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IArticleService : IBaseService<Article, ArticleDto, ArticleSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(ArticleDto dto);

        /// <summary>
        /// 增加点击数
        /// </summary>
        Task<bool> IncreaseVisitsAsync(long id, int count = 1);

        /// <summary>
        /// 点赞
        /// </summary>
        Task<bool> PraiseAsync(long id, long userId);

        /// <summary>
        /// 检查是否点赞
        /// </summary>
        Task<bool> HasUserPraisedAsync(long id, long userId);

        /// <summary>
        /// 收藏
        /// </summary>
        Task<bool> CollectAsync(long id, long userId);

        /// <summary>
        /// 取消收藏
        /// </summary>
        Task<bool> UnCollectAsync(long id, long userId);

        /// <summary>
        /// 检查是否收藏
        /// </summary>
        Task<bool> HasUserCollectedAsync(long id, long userId);
    }
}
