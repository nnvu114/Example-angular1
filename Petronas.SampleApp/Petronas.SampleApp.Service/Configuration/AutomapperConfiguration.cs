using AutoMapper;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Service.Configuration
{
    public class AutomapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Company, CompanyModel>();
                cfg.CreateMap<CompanyModel, Company>();
                cfg.CreateMap<NavigationModel, Navigation>();
                cfg.CreateMap<Navigation, NavigationModel>();
            });
        }
    }
}
