using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Vino.Core.CMS.Web.Application;

namespace Vino.Core.CMS.Web.Backend
{
    public class Startup : BackendStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {

        }
    }
}
