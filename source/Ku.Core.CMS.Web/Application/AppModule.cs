﻿using Autofac;
using Ku.Core.CMS.Service.UserCenter;
using Ku.Core.Infrastructure.DependencyResolver;
using Ku.Core.Tokens.Jwt;
using System;
using System.Reflection;

namespace Ku.Core.CMS.Web.Application
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacResolver>().As<IIocResolver>().InstancePerLifetimeScope();

            //Repository
            //builder.RegisterAssemblyTypes(typeof(BaseRepository<,>).GetTypeInfo().Assembly)
            //    .Where(t => t.Name.EndsWith("Repository", StringComparison.CurrentCultureIgnoreCase))
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency();

            //Service
            builder.RegisterAssemblyTypes(typeof(UserService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Service", StringComparison.CurrentCultureIgnoreCase) 
                        && !t.Name.EndsWith("SubscriberService", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //SubscriberService
            builder.RegisterAssemblyTypes(typeof(UserService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("SubscriberService", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<SystemJwtProvider>().As<IJwtProvider>().SingleInstance();
        }
    }
}
