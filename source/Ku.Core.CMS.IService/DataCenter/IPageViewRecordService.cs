//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IPageViewRecordService.cs
// 功能描述：页面浏览记录 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-08-28 10:37
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.DataCenter
{
    public partial interface IPageViewRecordService : IBaseService<PageViewRecord, PageViewRecordDto, PageViewRecordSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(PageViewRecordDto dto);
    }
}
