using Locker.Application;
using Locker.Infrastructure.Repositories;
using Locker.Infrastructure.Repositories.Interface;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Locker.Presentation.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
                        
            RegisterRepositories(container);
            RegisterApplication(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterApplication(UnityContainer container)
        {
            container.RegisterType<ITeste, Teste>(new PerResolveLifetimeManager());
        }

        private static void RegisterRepositories(UnityContainer container)
        {
            container.RegisterType<ILockerUnitOfWork, LockerUnitOfWork>(new PerResolveLifetimeManager());
        }
    }
}