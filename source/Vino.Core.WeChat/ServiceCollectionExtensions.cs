
using Vino.Core.WeChat.AccessToken;
using Vino.Core.WeChat.Qrcode;
using Vino.Core.WeChat.User;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeChat(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IWcAccessTokenTool), typeof(WcAccessTokenTool));
            services.AddSingleton(typeof(IWcUserTool), typeof(WcUserTool));
            services.AddSingleton(typeof(IWcQrcodeTool), typeof(WcQrcodeTool));
            return services;
        }
    }
}
