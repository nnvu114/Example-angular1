using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Data.Base;

namespace Petronas.SampleApp.Data.Classes.MasterData
{
    [Table("mdm.Company")]
    public class Company : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        public int CountryId { get; set; }

        public EnumCommon.EnumTimeFormat TimeFormatEnum { get; set; }


        //Navigations

        [ForeignKey("CountryId")]
        public virtual MdCountry MdCountry { get; set; }

    }
}
