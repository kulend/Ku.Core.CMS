//----------------------------------------------------------------
// Copyright (C) 2018 kulend 版权所有
//
// 文件名：MemberPointRecordService.cs
// 功能描述：会员积分记录 业务逻辑处理类
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-20 16:15
//
//----------------------------------------------------------------

using Ku.Core.CMS.Domain.Dto.Membership;
using Ku.Core.CMS.Domain.Entity.Membership;
using Ku.Core.CMS.IService.Membership;

namespace Ku.Core.CMS.Service.Membership
{
    public partial class MemberPointRecordService : BaseService<MemberPointRecord, MemberPointRecordDto, MemberPointRecordSearch>, IMemberPointRecordService
    {

    }
}
