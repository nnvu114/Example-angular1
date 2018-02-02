using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.Base;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Feedback;
using Petronas.SampleApp.Repository.Interfaces;
using Petronas.SampleApp.Service.Interfaces;

namespace Petronas.SampleApp.Service.Classes
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IRepository<T> _repository;

        protected EntityService(IUnitOfWork unitOfWork, IRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return _repository.GetAll();
        //}

        //public ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, EnumCommon.SortDirection direction)
        //{
        //    return _repository.GetAll(pageInfo, sortColumn, direction);
        //}

        //public IQueryable<T> FindQuery()
        //{
        //    return _repository.FindQuery();
        //}

        //public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        //{
        //    return _repository.FindBy(predicate);
        //}

        //public virtual T Create(T entity)
        //{
        //    if (entity == null)
        //    {
        //        throw new ArgumentNullException("entity");
        //    }
        //    T obj = _repository.Add(entity);
        //    _unitOfWork.Commit();
        //    return obj;
        //}

        //public ListFeedback<T> Create(IList<T> entityList)
        //{
        //    if (entityList == null)
        //    {
        //        throw new ArgumentNullException("entityList");
        //    }

        //    if (!entityList.Any())
        //    {
        //        return null;
        //        //throw new ArgumentOutOfRangeException("entityList");
        //    }

        //    ListFeedback<T> obj = _repository.Add(entityList);
        //    _unitOfWork.Commit();
        //    return obj;
        //}

        //public virtual void Update(T entity)
        //{
        //    if (entity == null) throw new ArgumentNullException("entity");
        //    _repository.Edit(entity);
        //    _unitOfWork.Commit();
        //}

        //public void Update(IList<T> entityList)
        //{
        //    if (entityList == null) throw new ArgumentNullException("entityList");

        //    if (!entityList.Any())
        //    {
        //        throw new ArgumentOutOfRangeException("entityList");
        //    }

        //    _repository.Edit(entityList);
        //    _unitOfWork.Commit();
        //}

        //public virtual void Delete(T entity)
        //{
        //    if (entity == null) throw new ArgumentNullException("entity");
        //    _repository.Delete(entity);
        //    _unitOfWork.Commit();
        //}

        //public void DeleteBy(Expression<Func<T, bool>> predicate)
        //{
        //    _repository.DeleteBy(predicate);
        //    _unitOfWork.Commit();
        //}
    }
}