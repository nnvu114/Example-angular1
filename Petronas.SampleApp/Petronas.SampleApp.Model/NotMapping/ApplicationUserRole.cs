using System;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Model.Classes.MasterData;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Petronas.SampleApp.Model.NotMapping
{
    public class ApplicationUserRole : IdentityRole
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public virtual Guid Id { get; set; }

        public DateTime InsertedAt { get; set; }

        public string InsertedBy { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}