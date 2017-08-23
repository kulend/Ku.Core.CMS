using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vino.Core.CMS.Web.Configs;
using Vino.Core.CMS.Web.Security;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BackendAuthExtensions
    {
        public static IServiceCollection AddBackendAuth(this IServiceCollection services, IConfiguration Configuration, IHostingEnvironment env)
        {
            services.Configure<JwtAuthConfig>(Configuration.GetSection("JwtAuth"));

            var key = Configuration["JwtSecKey:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // key 验证
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,

                // 验证发行者
                ValidateIssuer = true,
                ValidIssuer = Configuration["JwtAuth:Issuer"],

                // 验证使用者
                ValidateAudience = true,
                ValidAudience = Configuration["JwtAuth:Audience"],

                // 验证过期时间
                ValidateLifetime = true,

                // 时间偏差
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthorization(options =>
            {
                options.AddPolicy("auth", policy =>
                {
                    policy.Requirements.Add(new ValidJtiRequirement());
                    policy.Requirements.Add(new AuthAuthorizationRequirement());
                });
            });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(o =>
            //        {
            //            o.Authority = Configuration["JwtAuth:Issuer"];
            //            o.Audience = tokenValidationParameters.ValidAudience;
            //            //o.SecurityTokenValidators.Add();
            //            o.TokenValidationParameters = tokenValidationParameters;
            //        }
            //    );
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(o =>
            //        {
            //            o.LoginPath = new PathString(Configuration["JwtAuth:LoginPath"]);
            //            o.AccessDeniedPath = new PathString(Configuration["JwtAuth:AccessDeniedPath"]);
            //            o.Cookie.Name = Configuration["JwtAuth:CookieName"];
            //            o.TicketDataFormat = new JwtTicketDataFormat(
            //                SecurityAlgorithms.HmacSha256,
            //                tokenValidationParameters);
            //        }
            //    );
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = new PathString(Configuration["JwtAuth:LoginPath"]);
            //    options.AccessDeniedPath = new PathString(Configuration["JwtAuth:AccessDeniedPath"]);
            //});
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(o =>
            {
                o.LoginPath = new PathString(Configuration["JwtAuth:LoginPath"]);
                o.AccessDeniedPath = new PathString(Configuration["JwtAuth:AccessDeniedPath"]);
                o.Cookie.Name = Configuration["JwtAuth:CookieName"];
                o.TicketDataFormat = new JwtTicketDataFormat(
                    SecurityAlgorithms.HmacSha256,
                    tokenValidationParameters);
            });
            services.AddScoped<IAuthorizationHandler, ValidJtiHandler>();
            services.AddScoped<IAuthorizationHandler, BackendAuthAuthorizationHandler>();
            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class BackendAuthExtensions
    {
        public static IApplicationBuilder UseBackendAuth(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
