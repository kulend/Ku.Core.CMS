using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ku.Core.Tokens.Jwt;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtProvider, SystemJwtProvider>();
            services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}
