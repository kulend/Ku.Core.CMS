using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vino.Core.Tokens.Jwt;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtProvider, SystemJwtProvider>();
            return services.Configure<JwtSecKey>(configuration.GetSection("JwtSecKey"));
        }
    }
}
