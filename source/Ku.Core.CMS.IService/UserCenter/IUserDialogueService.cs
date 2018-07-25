//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserDialogueService.cs
// 功能描述：用户对话 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-07-25 10:23
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserDialogueService : IBaseService<UserDialogue, UserDialogueDto, UserDialogueSearch>
    {
        /// <summary>
        /// 新增
        /// </summary>
        Task AddAsync(long userId, string message);
    }
}
