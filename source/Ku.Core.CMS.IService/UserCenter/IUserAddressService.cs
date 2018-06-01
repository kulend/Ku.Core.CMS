//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IUserAddressService.cs
// 功能描述：收货地址 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-06-01 13:14
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.UserCenter;
using Ku.Core.CMS.Domain.Entity.UserCenter;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.UserCenter
{
    public partial interface IUserAddressService : IBaseService<UserAddress, UserAddressDto, UserAddressSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(UserAddressDto dto);
    }
}
