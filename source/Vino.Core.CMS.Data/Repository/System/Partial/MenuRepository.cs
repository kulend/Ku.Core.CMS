using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.Data;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    /// <summary>
    /// 菜单 仓储接口
    /// </summary>
    public partial interface IMenuRepository : IRepository<Menu>
    {
    }

    /// <summary>
    /// 菜单 仓储接口实现
    /// </summary>
    public partial class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
