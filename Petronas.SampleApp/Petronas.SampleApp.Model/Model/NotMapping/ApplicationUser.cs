using System;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Data.Classes.MasterData;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Petronas.SampleApp.Data.NotMapping
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpireDate { get; set; }

        public int? CompanyId { get; set; }

        //Navigations
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}