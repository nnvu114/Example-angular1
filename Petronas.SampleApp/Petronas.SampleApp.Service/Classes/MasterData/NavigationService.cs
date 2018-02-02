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

namespace Petronas.SampleApp.Service.Classes.MasterData
{
    public class NavigationService : EntityService<Navigation>, INavigationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Navigation> _repository;

        public NavigationService(IUnitOfWork unitOfWork,
            IRepository<Navigation> repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<List<MenuModel>> Get(Guid? Id)
        {
            List<MenuModel> data = new List<MenuModel>();
            try
            {
                var items = _repository.FindBy(s => s.ParentId == Id).OrderBy(s => s.Order).ToList();
                foreach (var item in items)
                {
                    MenuModel _menu = new MenuModel();
                    _menu.Name = item.Name;
                    _menu.Roles = item.Role.Split(',').ToList(); ;
                    _menu.Action = item.Action;
                    _menu.Url = item.Url;
                    _menu.Icon = item.Icon;
                    _menu.Children = new List<MenuModel>();
                    _menu.Children = await Get(item.Id);
                    //_menu.Roles = item.Roles;
                    data.Add(_menu);
                }
            }
            catch (Exception ex) {

            }
            
            return data;
        }
        public async Task<object> GetById(Guid Id)
        {
            var company = await _repository.FindBy(s => s.Id == Id).FirstOrDefaultAsync();
            return company;
        }

        public async Task<ResponseBaseModel> Add(NavigationModel model)
        {
            var obj = Mapper.Map<Navigation>(model);
            _repository.Add(obj);
            ResponseBaseModel ajaxResponse = new ResponseBaseModel();
            List<Navigation> _listReturn = new List<Navigation>();
            _listReturn.Add(obj);
            ajaxResponse.Data.data = _listReturn.ToList();
            ajaxResponse.Data.total = 1;
            ajaxResponse.statusCode = await _repository.CommitAsync();
            return ajaxResponse;
        }

        public async Task<ResponseBaseModel> Update(NavigationModel model)
        {
            var obj = await _repository.FindBy(m => m.Id == model.Id).FirstOrDefaultAsync();
            obj.Name = model.Name;
            obj.Description = model.Description;
            _repository.Update(obj);
            ResponseBaseModel returnObject = new ResponseBaseModel();
            returnObject.statusCode = await _repository.CommitAsync();
            List<NavigationModel> listItem = new List<NavigationModel>();
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