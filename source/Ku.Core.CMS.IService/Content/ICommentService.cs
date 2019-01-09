//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：ICommentService.cs
// 功能描述：评论 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2019-01-02 22:34
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Content;
using Ku.Core.CMS.Domain.Entity.Content;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Content
{
    public partial interface ICommentService : IBaseService<Comment, CommentDto, CommentSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(CommentDto dto);

        Task AddAsync(CommentDto dto);
    }
}
