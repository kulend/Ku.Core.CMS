using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    /// <summary>
    /// 功能 仓储接口
    /// </summary>
    public partial interface IFunctionRepository : IRepository<Function>
    {
    }

    /// <summary>
    /// 功能 仓储接口实现
    /// </summary>
    public partial class FunctionRepository : BaseRepository<Function>, IFunctionRepository
    {
        public FunctionRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
