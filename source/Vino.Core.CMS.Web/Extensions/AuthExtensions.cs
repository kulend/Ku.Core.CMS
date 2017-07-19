using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.CMS.Web.Security;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class JwtAuthExtensions
    {
        public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfigurationRoot Configuration)
        {
            services.Configure<JwtAuthConfig>(Configuration.GetSection("JwtAuth"));

            services.AddAuthorization(config =>
            {
                config.AddPolicy("permission", policy =>
                {
                    policy.Requirements.Add(new ValidJtiRequirement());
                    policy.Requirements.Add(new PermissionAuthorizationRequirement());
                });
            });

            services.AddScoped<IAuthorizationHandler, ValidJtiHandler>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class JwtAuthExtensions
    {
        public static IApplicationBuilder UseJwtAuth(this IApplicationBuilder app, IConfigurationRoot Configuration)
        {
            var jwtAuthConfig = app.ApplicationServices.GetService<IOptions<JwtAuthConfig>>().Value;

            var key = Configuration["JwtSecKey:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                // key 验证
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                // 验证发行者
                ValidateIssuer = true,
                ValidIssuer = jwtAuthConfig.Issuer,

                // 验证使用者
                ValidateAudience = true,
                ValidAudience = jwtAuthConfig.Audience,

                // 验证过期时间
                ValidateLifetime = true,

                // 时间偏差
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = new PathString(jwtAuthConfig.LoginPath),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = "Cookie",
                CookieName = jwtAuthConfig.CookieName,
                TicketDataFormat = new JwtTicketDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters)
            });
            return app;
        }
    }
}
