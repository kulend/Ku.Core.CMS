using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    public partial class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
