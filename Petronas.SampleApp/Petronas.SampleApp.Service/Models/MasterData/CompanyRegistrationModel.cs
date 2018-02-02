﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.NotMapping;

namespace Petronas.SampleApp.Service.Models.MasterData
{
    public class CompanyRegistrationModel
    {       
        public UserRegisterModel UserRegisterInfo { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "companyName")]
        public string Name { get; set; }

        public string ContactPersonName { get; set; }

        public string Address { get; set; }

        [StringLength(255)]
        public string Website { get; set; }

        [StringLength(20)]
        public string ContactNumber { get; set; }

        public string CompanyNumber { get; set; }

        public List<string> BusinessType { get; set; }

        public bool IsPetronasLicensedCompany { get; set; }

        public string RegistrationSWECCode { get; set; }

    }
}
