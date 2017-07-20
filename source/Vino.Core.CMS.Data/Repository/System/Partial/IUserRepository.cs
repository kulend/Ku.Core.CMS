using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    /// <summary>
    /// 用户管理仓储接口
    /// </summary>
    public partial interface IUserRepository : IRepository<User>
    {
    }
}
