using System;
using System.Collections.Generic;
using System.Text;
using Vino.Core.CMS.Core.DependencyResolver;
using Vino.Core.CMS.Domain.Entity.System;

namespace Vino.Core.CMS.Service.System
{
    public interface ILoginService
    {
        User DoLogin(string account, string password);

        User Create(string account, string password);
    }
}
