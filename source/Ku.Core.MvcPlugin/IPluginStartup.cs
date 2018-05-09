using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ku.Core.MvcPlugin
{
    public interface IPluginStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}
