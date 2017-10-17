
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.MvcPlugin;
using Vino.Core.CMS.Plugin.Article.Service;
using Vino.Core.CMS.Plugin.Article.Repository;

namespace Vino.Core.CMS.Plugin.Article
{
    public class PluginStartup : PluginBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IArticleService, ArticleService>();
            serviceCollection.AddTransient<IArticleRepository, ArticleRepository>();
        }
    }
}
