using System;
using System.Reflection;
using System.Configuration;

using Unity;
using Unity.Injection;
using Unity.RegistrationByConvention;
using Microsoft.Extensions.DependencyInjection;

using Starter.Data.Services;
using Starter.Data.ViewModels;
using Starter.Data.Connections;
using Starter.Data.Repositories;
using Starter.Framework.Clients;
using Starter.MessageBus.RabbitMQ;
using Starter.Repository.Connections;
using Starter.Repository.Repositories;

namespace Starter.Bootstrapper
{
    /// <summary>
    /// Sets up the dependency resolution for the project
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Sets the dependency resolution for the web project
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider Web(IServiceCollection services)
        {
            // Register all the dependencies
            Bootstrap();

            return IocWrapper.Instance.Container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Provides means to registry different service implementations
        /// based on the setup type
        /// </summary>
        public static void Bootstrap()
        {
            var container = new UnityContainer();

            var connection = ConfigurationManager.ConnectionStrings["DatabaseConnection"]?.ConnectionString;
            var apiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            var resourceUrl = ConfigurationManager.AppSettings["ResourceUrl"];

            container.RegisterType<IConnection, Connection>(new InjectionConstructor(connection));
            container.RegisterType<IApiClient, ApiClient>(new InjectionConstructor(apiUrl, resourceUrl));

            container.RegisterType<ICatRepository, CatRepository>();
            container.RegisterType<IMessageBus, RabbitMessageBus>();
            container.RegisterType<ICatService, CatService>();
            container.RegisterType<IMainViewModel, MainViewModel>();

            container.RegisterTypes(
                AllClasses.FromAssembliesInBasePath(),
                WithMappings.FromMatchingInterface,
                WithName.TypeName,
                WithLifetime.ContainerControlled,
                type =>
                {
                    // If settings type, load the setting
                    if (!type.IsAbstract && typeof(IConnection).IsAssignableFrom(type))
                    {
                        return new[]
                        {
                            new InjectionConstructor(connection)
                        };
                    }

                    // Otherwise, no special consideration is needed
                    return new InjectionMember[0];
                });

            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(),
            //    WithMappings.FromAllInterfaces,
            //    WithName.TypeName,
            //    WithLifetime.ContainerControlled
            //);
            //container.RegisterTypes(
            //    AllClasses.FromAssemblies(Assembly.GetExecutingAssembly()).Where(x =>
            //        x.IsPublic && x.GetInterfaces().Any() && (x.IsAbstract == false) &&
            //        x.IsClass), WithMappings.FromAllInterfacesInSameAssembly,
            //    type => container.Registrations.Select(x => x.RegisteredType)
            //        .Any(r => type.GetInterfaces().Contains(r))
            //        ? WithName.TypeName(type)
            //        : WithName.Default(type), WithLifetime.ContainerControlled);

            container.RegisterTypes(new InterfaceToTypeConvention(container, Assembly.GetExecutingAssembly()));
            
            IocWrapper.Instance = new IocWrapper(container);
        }
    }
}