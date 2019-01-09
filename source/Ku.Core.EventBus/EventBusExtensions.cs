﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ku.Core.EventBus;
using Ku.Core.EventBus.CAP;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEventBus<TContext>(this IServiceCollection services, IConfiguration Configuration)
            where TContext: DbContext
        {
            var config = Configuration.GetSection("EventBus");
            var provider = config["Provider"];
            EventBusConfig.Provider = provider;

            if (string.IsNullOrEmpty(provider) || "None".Equals(provider))
            {
                services.AddScoped<IEventPublisher, EmptyEventPublisher>();
            } else if ("CAP".Equals(provider, StringComparison.OrdinalIgnoreCase))
            {
                services.AddScoped<IEventPublisher, CapEventPublisher>();
                var capConfig = config.GetSection("CAP");
                //CAP
                services.AddCap(x =>
                {
                    // If your SqlServer is using EF for data operations, you need to add the following configuration：
                    x.UseEntityFramework<TContext>();

                    // If your Message Queue is using RabbitMQ you need to add the config：
                    var rabbitMQConfig = capConfig.GetSection("RabbitMQ");
                    x.UseRabbitMQ(opt =>
                    {
                        opt.HostName = rabbitMQConfig["HostName"];
                        opt.Port = int.Parse(rabbitMQConfig["Port"]);
                        opt.UserName = rabbitMQConfig["UserName"];
                        opt.Password = rabbitMQConfig["Password"];
                    });
                });
            }

            return services;
        }

    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class EventBusBuilderExtensions
    {
        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            if ("CAP".Equals(EventBusConfig.Provider, StringComparison.OrdinalIgnoreCase))
            {
                //app.UseCap();
            }
            return app;
        }
    }
}
