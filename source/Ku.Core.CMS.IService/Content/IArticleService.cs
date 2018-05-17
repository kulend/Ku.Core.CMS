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
    }
}
