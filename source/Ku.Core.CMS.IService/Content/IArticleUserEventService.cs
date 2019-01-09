//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IArticleUserEventService.cs
// 功能描述：文章用户事件 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-09-14 22:48
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface IArticleUserEventService : IBaseService<ArticleUserEvent, ArticleUserEventDto, ArticleUserEventSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(ArticleUserEventDto dto);
    }
}
