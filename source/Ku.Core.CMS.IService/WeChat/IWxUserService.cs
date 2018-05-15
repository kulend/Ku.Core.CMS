//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IWxUserService.cs
// 功能描述：微信用户 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;

namespace Ku.Core.CMS.IService.WeChat
{
    public partial interface IWxUserService : IBaseService<WxUser, WxUserDto, WxUserSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(WxUserDto dto);

        Task SaveRemarkAsync(WxUserDto dto);

        Task SyncAsync(long accountId);
    }
}
