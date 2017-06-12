using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Data.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public interface ILoginService : IDependency
    {
        User DoLogin(string account, string password);

        User Create(string account, string password);
    }
}
