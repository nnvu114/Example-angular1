using System;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Model.Classes.MasterData;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Petronas.SampleApp.Model.Base;
using Petronas.SampleApp.Model.Interfaces;

namespace Petronas.SampleApp.Model.NotMapping
{
    public class ApplicationUserRole : IdentityUserRole<Guid> {}
    public class ApplicationUserLogin : IdentityUserLogin<Guid> {}
    public class ApplicationUserClaim : IdentityUserClaim<Guid> {}
    public class ApplicationRole : IdentityRole<Guid, ApplicationUserRole> { }

    public class ApplicationUser : IdentityUser<Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpireDate { get; set; }

        public Guid? CompanyId { get; set; }

        //Navigations
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public DateTime? InsertedAt { get; set; }

        public string InsertedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
    
}