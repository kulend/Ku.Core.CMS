//----------------------------------------------------------------
// Copyright (C) 2018 vino 版权所有
//
// 文件名：AppFeedbackRepository.cs
// 功能描述：应用反馈 数据访问
//
// 创建者：kulend@qq.com
// 创建时间：2018-04-24 08:45
// 说明：此代码由工具自动生成，对此文件的更改可能会导致不正确的行为，
// 并且如果重新生成代码，这些更改将会丢失。
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.DataCenter;

namespace Vino.Core.CMS.Data.Repository.DataCenter
{
    /// <summary>
    /// 应用反馈 仓储接口
    /// </summary>
    public partial interface IAppFeedbackRepository : IRepository<AppFeedback>
    {
    }

    /// <summary>
    /// 应用反馈 仓储接口实现
    /// </summary>
    public partial class AppFeedbackRepository : BaseRepository<AppFeedback>, IAppFeedbackRepository
    {
        public AppFeedbackRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
