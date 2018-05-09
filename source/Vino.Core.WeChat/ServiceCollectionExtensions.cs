
using Ku.Core.WeChat.AccessToken;
using Ku.Core.WeChat.Qrcode;
using Ku.Core.WeChat.User;

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
