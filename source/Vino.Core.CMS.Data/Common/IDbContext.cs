using System;
using System.Collections.Generic;
using System.Text;

namespace Vino.Core.CMS.Data.Common
{
    public interface IDbContext
    {
        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
    }
}
