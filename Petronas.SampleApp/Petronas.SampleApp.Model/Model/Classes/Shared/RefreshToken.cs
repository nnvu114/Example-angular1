﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Data.Model.Base;

namespace Petronas.SampleApp.Data.Classes.Shared
{
    [Table("RefreshToken")]
    public class RefreshToken: BaseEntity
    {
        [Key]
        public string Id { get; set; }

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
    }
}