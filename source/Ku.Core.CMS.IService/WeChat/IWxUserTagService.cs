//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IWxUserTagService.cs
// 功能描述：微信用户标签 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.WeChat;
using Ku.Core.CMS.Domain.Entity.WeChat;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.WeChat
{
    public partial interface IWxUserTagService : IBaseService<WxUserTag, WxUserTagDto, WxUserTagSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(WxUserTagDto dto);
    }
}
