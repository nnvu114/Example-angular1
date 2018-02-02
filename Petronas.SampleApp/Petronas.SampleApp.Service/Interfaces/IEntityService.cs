using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.Base;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Feedback;

namespace Petronas.SampleApp.Service.Interfaces
{
    public interface IEntityService<T> where T : BaseEntity
    {
        //IEnumerable<T> GetAll();
        //ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, EnumCommon.SortDirection direction);
        //IQueryable<T> FindQuery();
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        //T Create(T entity);
        //ListFeedback<T> Create(IList<T> entityList);
        //void Delete(T entity);
        //void DeleteBy(Expression<Func<T, bool>> predicate);
        //void Update(T entity);

        //void Update(IList<T> entityList);
    }
}