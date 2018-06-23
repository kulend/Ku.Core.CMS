//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserPointRecordService.cs
// 功能描述：用户积分记录 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-23 15:30
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserPointRecordService : IBaseService<UserPointRecord, UserPointRecordDto, UserPointRecordSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserPointRecordDto dto);
    }
}
