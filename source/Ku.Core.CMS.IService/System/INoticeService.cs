//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：INoticeService.cs
// 功能描述：公告 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-13 22:23
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.System;
using Ku.Core.CMS.Domain.Entity.System;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.System
{
    public partial interface INoticeService : IBaseService<Notice, NoticeDto, NoticeSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(NoticeDto dto);
    }
}
