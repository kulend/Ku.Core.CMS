using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Data.Repository.System
{
    public partial class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(VinoDbContext dbcontext) : base(dbcontext)
        {
        }
    }
}
