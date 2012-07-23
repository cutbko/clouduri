using System.Web.Mvc;
using CloudUri.DAL;
using CloudUri.DAL.Database;
using CloudUri.DAL.Repository;
using CloudUri.SAL.Services;
using CloudUri.SAL.Services.Stubs;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace CloudUri.Web
{
    /// <summary>
    /// Initializes unity dependency resolver
    /// </summary>
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            IUnityContainer container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IDbWrapper, SqlDbWrapper>();
            container.RegisterType<IDALContext, SqlDALContext>();
            container.RegisterType<IAccountService, AccountServiceStub>();
            container.RegisterType<IDevicesService, DevicesServiceStub>();
            container.RegisterType<IFeedsService, FeedsServiceStub>();

            return container;
        }
    }
}