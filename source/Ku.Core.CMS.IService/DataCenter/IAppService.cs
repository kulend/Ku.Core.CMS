//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAppService.cs
// 功能描述：应用 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-15 10:34
//
//----------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;

namespace Ku.Core.CMS.IService.DataCenter
{
    public partial interface IAppService : IBaseService<App, AppDto, AppSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AppDto dto);

        /// <summary>
        /// 根据Appkey取得应用信息
        /// </summary>
        Task<AppDto> GetByAppkeyAsync(string appkey);
    }
}
