//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMemberService.cs
// 功能描述：会员 业务逻辑接口
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
    public partial interface IMemberService : IBaseService<Member, MemberDto, MemberSearch>
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        Task SaveAsync(MemberDto dto);
    }
}
