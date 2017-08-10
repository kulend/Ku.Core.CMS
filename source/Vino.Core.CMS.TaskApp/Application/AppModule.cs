using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Vino.Core.CMS.Data.Common;
using Vino.Core.CMS.Service.System;
using Vino.Core.Infrastructure.DependencyResolver;

namespace Vino.Core.CMS.TaskApp.Application
{
    public class AppModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacResolver>().As<IIocResolver>().SingleInstance();

            //Repository
            builder.RegisterAssemblyTypes(typeof(BaseRepository<,>).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Repository", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();
            //Service
            builder.RegisterAssemblyTypes(typeof(IUserService).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("Service", StringComparison.CurrentCultureIgnoreCase))
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
