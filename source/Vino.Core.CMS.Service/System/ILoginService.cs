using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Data.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public interface ILoginService : IDependency
    {
        Operator DoLogin(string account, string password);

        Operator Create(string account, string password);
    }
}
