using Petronas.SampleApp.Model;
using Petronas.SampleApp.Model.NotMapping;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using Petronas.SampleApp.Common.Helpers;
using Petronas.SampleApp.Service.Classes.Shared;

namespace Petronas.SampleApp.WebApi
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser,Guid>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser,Guid> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context.Get<SampleAppContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser,Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    //Code for email confirmation and reset password life time
                    TokenLifespan = TimeSpan.FromHours(Petronas.SampleApp.Common.Constants.Configuration.LinkViaEmailLifeTime)
                };          
            }

            return manager;
        }
    }   
}
