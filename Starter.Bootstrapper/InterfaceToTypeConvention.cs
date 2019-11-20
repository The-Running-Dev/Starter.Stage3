using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Unity;
using Unity.Lifetime;
using Unity.Injection;
using Unity.RegistrationByConvention;

namespace Starter.Bootstrapper
{
    public class InterfaceToTypeConvention : RegistrationConvention
    {
        public InterfaceToTypeConvention(IUnityContainer unity, params Assembly[] assemblies) : this(unity, assemblies.SelectMany(a => a.GetExportedTypes()).ToArray())
        {
            _unity = unity;
        }

        public InterfaceToTypeConvention(IUnityContainer unity, params Type[] types)
        {
            _unity = unity;
            _types = types ?? Enumerable.Empty<Type>();
        }

        public override Func<Type, IEnumerable<Type>> GetFromTypes()
        {
            return (WithMappings.FromAllInterfacesInSameAssembly);
        }

        public override Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers()
        {
            return (x => Enumerable.Empty<InjectionMember>());
        }

        public override Func<Type, ITypeLifetimeManager> GetLifetimeManager()
        {
            return (WithLifetime.ContainerControlled);
        }

        public override Func<Type, string> GetName()
        {
            return (type => _unity.Registrations.Select(x => x.RegisteredType).Any(r => type.GetInterfaces().Contains(r))
                ? WithName.TypeName(type)
                : WithName.Default(type));
        }

        public override IEnumerable<Type> GetTypes()
        {
            return (_types.Where(x => x.IsPublic && x.GetInterfaces().Any() && (x.IsAbstract == false) && x.IsClass));
        }

        private readonly IUnityContainer _unity;

        private readonly IEnumerable<Type> _types;
    }
}