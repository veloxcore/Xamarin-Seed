using Autofac;
using System;
using System.Reflection;

namespace XamarinSeed.ViewModel
{
    public class Bootstrap
    {
        public static void RegisterDependency(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Bootstrap).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("ViewModel")).AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterType<Mediator.Mediator>().As<Mediator.IMediator>().SingleInstance();

            XamarinSeed.Service.Bootstrap.RegisterDependency(builder);
        }
    }
}
