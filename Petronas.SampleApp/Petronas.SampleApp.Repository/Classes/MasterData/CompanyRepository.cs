using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Repository.Interfaces.MasterData;
using System.Linq;

namespace Petronas.SampleApp.Repository.Classes.MasterData
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context)
            : base(context)
        {           
        }

        public Company FindCompanyByCode(string companyNumber)
        {
            return FindBy(x => x.CompanyNumber.Equals(companyNumber)).FirstOrDefault<Company>();
        }

        public Guid FindUserIdByCompanyId(Guid companyId)
        {
            Task<Company> company = FindBy(x => x.Id == companyId).FirstOrDefaultAsync<Company>();

            if (company.Result != null)
            {
                var user = company.Result.Users.ToList<ApplicationUser>().FirstOrDefault();

                if (user != null)
                {
                    return user.Id;
                }
            }

            return Guid.Empty;
        }
    }
}