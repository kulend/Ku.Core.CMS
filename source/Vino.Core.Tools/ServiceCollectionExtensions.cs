using System;
using System.Runtime.InteropServices.ComTypes;
using QRCoder;
using Vino.Core.Tools.QRCode;
using Vino.Core.Tools.VerificationCode;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTools(this IServiceCollection services)
        {
            services.AddTransient<IQrCodeGen, QrCodeGen>();
            services.AddTransient<IVerificationCodeGen, VerificationCodeGen>();
            return services;
        }
    }
}
