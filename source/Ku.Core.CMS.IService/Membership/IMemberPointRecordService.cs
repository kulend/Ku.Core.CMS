//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：IMemberPointRecordService.cs
// 功能描述：会员积分记录 业务逻辑接口
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 16:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;

namespace Ku.Core.CMS.IService.Membership
{
    public partial interface IMemberPointRecordService : IBaseService<MemberPointRecord, MemberPointRecordDto, MemberPointRecordSearch>
    {
    }
}
