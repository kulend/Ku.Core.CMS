//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMemberAddressService.cs
// 功能描述：会员地址 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-02 14:29
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Membership
{
    public partial interface IMemberAddressService : IBaseService<MemberAddress, MemberAddressDto, MemberAddressSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MemberAddressDto dto);
    }
}
