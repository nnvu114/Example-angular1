using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Model.Base;

namespace Petronas.SampleApp.Model.Classes.Shared
{
    [Table("RefreshToken")]
    public class RefreshToken: BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }
        public string RefreshTokenId { get; set; }
    }
}