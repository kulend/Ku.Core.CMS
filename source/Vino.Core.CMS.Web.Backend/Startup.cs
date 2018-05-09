using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Ku.Core.CMS.Web.Application;

namespace Ku.Core.CMS.Web.Backend
{
    public class Startup : BackendStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {

        }
    }
}
