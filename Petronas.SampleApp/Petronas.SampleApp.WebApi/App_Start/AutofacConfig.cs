using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Petronas.SampleApp.Model;
using Petronas.SampleApp.Repository.Classes;
using Petronas.SampleApp.Repository.Interfaces;
using Petronas.SampleApp.Repository.Providers;

namespace Petronas.SampleApp.WebApi
{
    /// <summary>
    /// Autofac configuration
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// Config function
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            // Base set-up
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Register dependencies
            SetUpRegistration(builder);

            // Build registration.
            var container = builder.Build();

            // Set the dependency resolver to be Autofac.
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void SetUpRegistration(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("Petronas.SampleApp.Repository"))
                .Where(t => t.Name.Contains("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("Petronas.SampleApp.Service"))
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType(typeof(SampleAppContext)).As(typeof(DbContext)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();
            builder.RegisterType(typeof(DefaultCacheProvider)).As(typeof(ICacheProvider)).InstancePerRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }
    }
}