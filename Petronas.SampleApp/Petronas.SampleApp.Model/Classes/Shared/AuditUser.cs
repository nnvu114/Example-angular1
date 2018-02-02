using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.Base;

namespace Petronas.SampleApp.Model.Classes.Shared
{
    [Table("AuditClient")]
    public class AuditUser : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public EnumCommon.ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        [MaxLength(100)]
        public string AllowedOrigin { get; set; }
    }
}