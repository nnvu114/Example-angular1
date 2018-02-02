using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;

namespace Petronas.SampleApp.Service.Interfaces.MasterData
{
    public interface INavigationService : IEntityService<Navigation>
    {
        Task<List<MenuModel>> Get(Guid? Id);
        Task<object> GetById(Guid Id);
        Task<ResponseBaseModel> Add(NavigationModel model);
        Task<ResponseBaseModel> Update(NavigationModel model);
        Task<int> Delete(Guid id);
        ResponseBaseModel Search(PageInfo pageInfo);
    }
}