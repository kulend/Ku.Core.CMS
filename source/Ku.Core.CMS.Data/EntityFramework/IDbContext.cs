using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.CMS.Data.EntityFramework
{
    public interface IDbContext
    {
        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
    }
}
