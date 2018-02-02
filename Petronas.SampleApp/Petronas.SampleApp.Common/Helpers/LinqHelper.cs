using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Petronas.SampleApp.Common.Helpers
{
    public static class LinqHelper
    {
        // <summary>Orders the sequence by specific column and direction.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="direction">asc/desc</param>
        public static IQueryable<T> OrderByCustom<T>(this IQueryable<T> query, string sortColumn, EnumCommon.SortDirection direction)
        {
            var methodName = string.Format("OrderBy{0}", direction == EnumCommon.SortDirection.Ascending ? "" : "descending");

            var parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in sortColumn.Split('.'))
            {
                memberAccess = MemberExpression.Property(memberAccess ?? (parameter as Expression), property);
            }

            var orderByLambda = Expression.Lambda(memberAccess, parameter);

            var result = Expression.Call(
                typeof(Queryable),
                methodName,
                new[] { query.ElementType, memberAccess.Type },
                query.Expression,
                Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }
        public static IQueryable<T> FilterCustom<T>(this IQueryable<T> queryable,string fieldName, string keywords)
        {
            var entityType = typeof(T);
            var parameter = Expression.Parameter(entityType, "a");
            var containsMethod = typeof(string).GetMethod("Contains"
                                                           , new[] { typeof(string) });
            var propertyExpression = Expression.Property(parameter, fieldName);
            Expression body = Expression.Constant(false);
            //foreach (var keyword in keywords)
            //{
            //    var innerExpression = Expression.Call(propertyExpression, containsMethod, Expression.Constant(keyword));
            //    body = Expression.OrElse(body, innerExpression);
            //}
            var innerExpression = Expression.Call(propertyExpression, containsMethod, Expression.Constant(keywords));
            body = Expression.OrElse(body, innerExpression);
            var lambda = Expression.Lambda<Func<T, bool>>(body, new[] { parameter });
            return queryable.Where(lambda);
        }
    }
}