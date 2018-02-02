using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Data.Model.Base;

namespace Petronas.SampleApp.Data.Model.Classes.Shared
{
    [Table("AuditClient")]
    public class AuditUser : BaseEntity
    {
        [Key]
        public string Id { get; set; }
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