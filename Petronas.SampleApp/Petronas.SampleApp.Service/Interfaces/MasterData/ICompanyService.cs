using System;
using System.Threading.Tasks;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;

namespace Petronas.SampleApp.Service.Interfaces.MasterData
{
    public interface ICompanyService : IEntityService<Company>
    {
        Task<object> Get();
        Task<object> GetById(Guid Id);
        Task<ResponseBaseModel> Add(CompanyRegistrationModel model);
        Task<ResponseBaseModel> Update(CompanyModel model);
        Task<int> Delete(Guid id);
        ResponseBaseModel Search(PageInfo pageInfo);

        Guid FindUserIdByCompanyId(Guid companyId);
    }
}