using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Petronas.SampleApp.Model;
using Petronas.SampleApp.Model.Migrations;
using Petronas.SampleApp.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using Petronas.SampleApp.Service.Configuration;

[assembly: OwinStartup(typeof(Startup))]

namespace Petronas.SampleApp.WebApi
{
    /// <summary>
    ///     Start up
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Configuration IAppBuilder
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            AutofacConfig.Configure(config);
            AuthConfig.Configure(app);

            //WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleAppContext, Configuration>());

            //old
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutomapperConfiguration.Configure();

            ConfigureOAuthTokenGeneration(app);
            //Repository.Classes.Shared.AuthRepository authorRepo = new Repository.Classes.Shared.AuthRepository(new SampleAppContext());
            //authorRepo.CreateDefaultRolesandUsers();
        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request    
            app.CreatePerOwinContext(SampleAppContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
        }
    }
}