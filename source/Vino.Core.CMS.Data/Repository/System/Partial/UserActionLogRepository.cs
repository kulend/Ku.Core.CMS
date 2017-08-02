using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    /// <summary>
    /// 用户操作日志 仓储接口
    /// </summary>
    public partial interface IUserActionLogRepository : IRepository<UserActionLog>
    {
    }

    /// <summary>
    /// 用户操作日志 仓储接口实现
    /// </summary>
    public partial class UserActionLogRepository : BaseRepository<UserActionLog>, IUserActionLogRepository
    {
        public UserActionLogRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
