using Ku.Core.CMS.Web.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Ku.Core.CMS.Frontend.PcSite
{
    public class Startup : PcSiteStartup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {

        }
    }
}
