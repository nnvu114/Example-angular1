using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Common.Helpers;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.NotMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Petronas.SampleApp.Model.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<SampleAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true; //for client team
            //AutomaticMigrationsEnabled = false; //for server team
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SampleAppContext context)
        {
            //This method will be called after migrating to the latest version.
            //build initial master data
            //context.MdCountries.AddRange(BuildCountries.Build());
            //foreach (var user in BuildUsers.Build())
            //{
            //    context.Users.AddOrUpdate(u => u.UserName, user);
            //}
            if (context.AuditUsers.Count() > 0)
            {
                return;
            }
            context.AuditUsers.AddRange(BuildClientsList());
            context.SaveChanges();

            AddUserAndRole();
        }
        private static List<AuditUser> BuildClientsList()
        {

            List<AuditUser> ClientsList = new List<AuditUser>
            {
                new AuditUser
                { Id = Guid.NewGuid(),
                    Secret= CommonHelper.GetHash("abc@123"),
                    Name="AngularJS front-end Application",
                    ApplicationType = EnumCommon.ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
                    AllowedOrigin = "*"
                },
                new AuditUser
                { Id = Guid.NewGuid(),
                    Secret=CommonHelper.GetHash("123@abc"),
                    Name="Console Application",
                    ApplicationType = EnumCommon.ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                }
            };

            return ClientsList;
        }

        public void AddUserAndRole()
        {
            var userStore = new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(new SampleAppContext());
            var manager = new UserManager<ApplicationUser, Guid>(userStore);
     
            var roleStore = new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(new SampleAppContext());
            var roleManager = new RoleManager<ApplicationRole, Guid>(roleStore);
           
            var passwordHash = new PasswordHasher();
            var password = passwordHash.HashPassword("Test@123");

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                PasswordHash = password,
                PhoneNumber = "0987888999",
                FirstName = "Super",
                LastName = "Administrator",
                Email = "admin@Petronas.SampleApp.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ExpireDate = DateTime.Now.AddDays(90)
            };

            manager.Create(user, password);
            userStore.Context.SaveChanges();

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new ApplicationRole { Id = Guid.NewGuid(), Name = Role.Admin });
                roleManager.Create(new ApplicationRole { Id = Guid.NewGuid(), Name = Role.ExternalUser });
            }
            roleStore.Context.SaveChanges();

            var adminUser = manager.FindByName("admin");

            manager.AddToRole(adminUser.Id, Role.Admin);
            userStore.Context.SaveChanges();
        }
    }
}
