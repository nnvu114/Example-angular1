using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Model.NotMapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petronas.SampleApp.Repository.Interfaces.MasterData
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Company FindCompanyByCode(string companyNumber);
        Guid FindUserIdByCompanyId(Guid companyId);
    }
}