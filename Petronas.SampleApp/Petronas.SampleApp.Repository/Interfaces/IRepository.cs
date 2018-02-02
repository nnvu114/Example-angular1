using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Model.Base;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Feedback;

namespace Petronas.SampleApp.Repository.Interfaces
{

    public interface IRepository<T> where T : BaseEntity
    {
        //find
        IQueryable<T> FindQuery();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);


        //get all
        IEnumerable<T> GetAll();
        ListFeedback<T> GetAll(PageInfo pageInfo, string sortColumn, EnumCommon.SortDirection direction);
        ListFeedback<T> GetByQuery(PageInfo pageInfo);

        //add
        T Add(T entity);
        ListFeedback<T> Add(IList<T> entityList);
        
        
        //update
        void Update(T entity);
        void Update(IList<T> entityList);
        
        
        //delete
        T Delete(T entity);
        void DeleteBy(Expression<Func<T, bool>> predicate);


        //commit
        void Commit();
        Task<int> CommitAsync();
    }
}