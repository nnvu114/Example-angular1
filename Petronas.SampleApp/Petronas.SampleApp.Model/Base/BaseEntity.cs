using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Model.Interfaces;

namespace Petronas.SampleApp.Model.Base
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        public DateTime? InsertedAt { get; set; }

        public string InsertedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}