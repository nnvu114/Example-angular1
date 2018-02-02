using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Petronas.SampleApp.Model;
using Petronas.SampleApp.Model.NotMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Petronas.SampleApp.WebApi
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> roleStore) : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context.Get<SampleAppContext>()));
           
            return appRoleManager;
        }
    }
}