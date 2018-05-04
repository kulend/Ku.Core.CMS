//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：MemberAddressRepository.cs
// 功能描述：会员地址 数据访问
//
// 创建者：kulend@qq.com
// 创建时间：2018-05-02 14:29
// 说明：此代码由工具自动生成，对此文件的更改可能会导致不正确的行为，
// 并且如果重新生成代码，这些更改将会丢失。
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.Membership;

namespace Vino.Core.CMS.Data.Repository.Membership
{
    /// <summary>
    /// 会员地址 仓储接口
    /// </summary>
    public partial interface IMemberAddressRepository : IRepository<MemberAddress>
    {
    }

    /// <summary>
    /// 会员地址 仓储接口实现
    /// </summary>
    public partial class MemberAddressRepository : BaseRepository<MemberAddress>, IMemberAddressRepository
    {
        public MemberAddressRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
