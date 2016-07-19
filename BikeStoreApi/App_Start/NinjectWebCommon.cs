[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BikeStoreApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BikeStoreApi.App_Start.NinjectWebCommon), "Stop")]


namespace BikeStoreApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using Platform.Client.Interfaces;
    using Platform.Client.Mocks;
    using BikeStoreApi.Interfaces;
    using BikeStoreApi.Composers;
    using WebApiContrib.IoC.Ninject;
    using System.Web.Http;
    using Platform.Client.Services;
    using Domain;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
            
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);


                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);


                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IOfferMockServiceClient>().To<MockOffersServiceClient>();

            kernel.Bind<IDistributorsServiceClient>().To<DistributorsServiceClient>();
            kernel.Bind<IOfferServiceClient>().To<OfferServiceClient>();
            kernel.Bind<IDiscountServiceClient>().To<DiscountServiceClient>();
            kernel.Bind<ILineServiceClient>().To<LineServiceClient>();

            kernel.Bind<IDiscountComposer>().To<DiscountComposer>();
            kernel.Bind<IDistributorComposer>().To<DistributorsComposer>();
            kernel.Bind<IOffersComposer>().To<OffersComposer>();

        }        
    }
}
