using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ku.Core.CMS.Web.Configs;
using Ku.Core.CMS.Web.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class WebApiAuthExtensions
    {
        public static IServiceCollection AddWebApiAuth(this IServiceCollection services, IConfiguration Configuration, IHostingEnvironment env)
        {
            var key = Configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // key 验证
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                // 验证发行者
                ValidateIssuer = true,
                ValidIssuer = Configuration["Jwt:Issuer"],

                // 验证使用者
                ValidateAudience = true,
                ValidAudience = Configuration["Jwt:Audience"],

                // 验证过期时间
                ValidateLifetime = true,

                // 时间偏差
                ClockSkew = TimeSpan.Zero
            };
 
            services.AddAuthorization(options =>
            {
                options.AddPolicy("auth", policy =>
                {
                    policy.Requirements.Add(new WebApiAuthAuthorizationRequirement());
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => {
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddScoped<IAuthorizationHandler, WebApiAuthAuthorizationHandler>();
            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApiAuthExtensions
    {
        public static IApplicationBuilder UseWebApiAuth(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
