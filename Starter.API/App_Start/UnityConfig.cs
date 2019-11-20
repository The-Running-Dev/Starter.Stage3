using System.Web.Http;

using Unity.WebApi;

using Starter.Bootstrapper;

namespace Starter.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			Setup.Bootstrap();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(IocWrapper.Instance.Container);
        }
    }
}