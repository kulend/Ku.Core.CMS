//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMemberTypeService.cs
// 功能描述：会员类型 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-02-04 19:13
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using System.Threading.Tasks;

namespace Ku.Core.CMS.IService.Membership
{
    public partial interface IMemberTypeService : IBaseService<MemberType, MemberTypeDto, MemberTypeSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MemberTypeDto dto);
    }
}
