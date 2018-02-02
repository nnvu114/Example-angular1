using System;
using System.Linq;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes.MasterData;
using Petronas.SampleApp.Repository.Interfaces;
using Petronas.SampleApp.Service.Interfaces.MasterData;
using System.Data.Entity;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Common.Helpers;
using Petronas.SampleApp.Repository.Interfaces.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;
using Petronas.SampleApp.Common.Models;
using System.Collections.Generic;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Feedback;
using AutoMapper;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Repository.Interfaces.Shared;
using System.Net;
using Microsoft.AspNet.Identity;

namespace Petronas.SampleApp.Service.Classes.MasterData
{
    public class CompanyService : EntityService<Company>, ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _repository;
        private readonly IAuthRepository _authorRepository;

        public CompanyService(IUnitOfWork unitOfWork,
            ICompanyRepository repository, IAuthRepository authorRepository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _authorRepository = authorRepository;
        }

        public async Task<object> Get()
        {
            var company = await _repository.FindQuery()
                .Select(m => new CompanyModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Address = m.Address,
                    Website = m.Website,
                    ContactNumber = m.ContactNumber,
                })
                .FirstOrDefaultAsync();
            return company;
        }
        public async Task<object> GetById(Guid Id)
        {
            var company = await _repository.FindBy(s => s.Id == Id).FirstOrDefaultAsync();
            return company;
        }

        public Guid FindUserIdByCompanyId(Guid companyId)
        {
            return _repository.FindUserIdByCompanyId(companyId);
        }

        public async Task<ResponseBaseModel> Add(CompanyRegistrationModel model)
        {
            ResponseBaseModel ajaxResponse = new ResponseBaseModel();
            ApplicationUser user = await _authorRepository.FindUserByEmail(model.UserRegisterInfo.Email);

            if (user != null)
            {
                ajaxResponse.statusCode = (int)HttpStatusCode.Conflict;
                ajaxResponse.statusText = Common.Constants.CompanyMessage.ExistedEmail;

                return ajaxResponse;
            }

            user = await _authorRepository.FindUser(model.UserRegisterInfo.UserName);
            if (user != null)
            {
                ajaxResponse.statusCode = (int)HttpStatusCode.Conflict;
                ajaxResponse.statusText = Common.Constants.CompanyMessage.ExistedUsername;

                return ajaxResponse;
            }

            Company company = _repository.FindCompanyByCode(model.CompanyNumber);
            if (company != null)
            {
                ajaxResponse.statusCode = (int)HttpStatusCode.Conflict;
                ajaxResponse.statusText = Common.Constants.CompanyMessage.ExistedCompanyNo;

                return ajaxResponse;
            }

            Company obj = new Company
            {
                Name = model.Name,
                ContactPersonName = model.ContactPersonName,
                Address = model.Address,
                Website = model.Website,
                ContactNumber = model.ContactNumber,
                CompanyNumber = model.CompanyNumber,
                BusinessType = model.BusinessType,
                IsPetronasLicensedCompany = model.IsPetronasLicensedCompany,
                RegistrationSWECCode = model.RegistrationSWECCode
            };

            //var obj = Mapper.Map<Company>(model);
            var insertedCompany = _repository.Add(obj);
            int code = await _repository.CommitAsync();

            IdentityResult res = null;

            if (code != 0)
            {
                res = await _authorRepository.RegisterExternalUser(model.UserRegisterInfo, insertedCompany.Id);
            }

            List<Company> _listReturn = new List<Company>();
            _listReturn.Add(insertedCompany);
            ajaxResponse.Data.data = _listReturn.ToList();
            ajaxResponse.Data.total = 1;
            if (res.Succeeded)
            {
                ajaxResponse.statusCode = code;
            }
            else
            {
                ajaxResponse.statusCode = 0;
            }
            return ajaxResponse;
        }     

        public async Task<ResponseBaseModel> Update(CompanyModel model)
        {
            var obj = await _repository.FindBy(m => m.Id == model.Id).FirstOrDefaultAsync();
            obj.Name = model.Name;          
            obj.Address = model.Address;
            obj.Website = model.Website;
            obj.ContactNumber = model.ContactNumber;
            _repository.Update(obj);
            ResponseBaseModel returnObject = new ResponseBaseModel();
            returnObject.statusCode = await _repository.CommitAsync();
            List<CompanyModel> listItem = new List<CompanyModel>();
            listItem.Add(model);
            returnObject.Data.data = listItem;
            returnObject.Data.total = 1;
            return returnObject;
        }
        public ResponseBaseModel Search(PageInfo pageInfo)
        {
            ResponseBaseModel result = new ResponseBaseModel();
            var feedback = _repository.GetByQuery(pageInfo);
            result.Data.total = feedback.Total;
            result.Data.data = feedback.Data;
            return result;
        }

        public async Task<int> Delete(Guid id)
        {
            _repository.DeleteBy(m => m.Id == id);
            return await _repository.CommitAsync();
        }
    }
}