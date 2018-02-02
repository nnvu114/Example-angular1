using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.Base;

namespace Petronas.SampleApp.Model.Classes.MasterData
{
    [Table("Navigation")]
    public class Navigation : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Order { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public bool Action { get; set; }
        public string Url { get; set; }

        public Guid? ParentId { get; set; }
        public string Role { get; set; }
    }
}
