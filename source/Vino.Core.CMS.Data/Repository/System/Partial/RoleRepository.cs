using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System.Partial
{
    public partial class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(VinoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
