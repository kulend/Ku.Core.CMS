using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Service.System;
using Vino.Core.Tokens.Jwt;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.Web.Application
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VinoDbContext>().As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<AutofacResolver>().As<IIocResolver>().InstancePerLifetimeScope();

            //Repository
            builder.RegisterAssemblyTypes(typeof(BaseRepository<,>).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Repository", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            //Service
            builder.RegisterAssemblyTypes(typeof(IUserService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Service", StringComparison.CurrentCultureIgnoreCase) 
                        && !t.Name.EndsWith("SubscriberService", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //SubscriberService
            builder.RegisterAssemblyTypes(typeof(IUserService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("SubscriberService", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<SystemJwtProvider>().As<IJwtProvider>().SingleInstance();
        }
    }
}
