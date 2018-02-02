using System;
using System.Collections.Generic;
using Petronas.SampleApp.Common;

namespace Petronas.SampleApp.Service.Models.MasterData
{
    public class NavigationModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }

        public bool Action { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }

        public Guid? ParentId { get; set; }
        public List<Guid> RolesId { get; set; }
        public List<string> RoleName { get; set; }
        public NavigationModel()
        {
            RoleName = new List<string>();
            RolesId = new List<Guid>();
        }
    }
    public class MenuModel
    {
        public string Name { get; set; }
        public bool Action { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public List<string> Roles { get; set; }
        public List<MenuModel> Children { get; set; }
    }
}
