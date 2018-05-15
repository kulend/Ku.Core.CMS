//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IAppVersionService.cs
// 功能描述：应用版本 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-16 10:59
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.DataCenter;
using Ku.Core.CMS.Domain.Entity.DataCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.DataCenter
{
    public partial interface IAppVersionService : IBaseService<AppVersion, AppVersionDto, AppVersionSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(AppVersionDto dto);
    }
}
