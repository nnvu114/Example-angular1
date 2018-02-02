using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Data.Model.Base;

namespace Petronas.SampleApp.Data.Model.Classes.MasterData
{
    [Table("mdm.MdCountry")]
    public class MdCountry : BaseEntity
    {
        public MdCountry() { }

        public MdCountry(string name, string description, string defaultCurrency, string defaultTimezone)
        {
            Name = name;
            Description = description;
            DefaultCurrency = defaultCurrency;
            DefaultTimezone = defaultTimezone;
            InsertedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            IsDeleted = false;
        }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string DefaultCurrency { get; set; }

        [StringLength(255)]
        public string DefaultTimezone { get; set; }


        //Navigations
        public virtual ICollection<Company> Companies { get; set; }
    }
}
