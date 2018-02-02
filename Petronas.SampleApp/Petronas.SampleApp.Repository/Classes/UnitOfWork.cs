using System;
using System.Data.Entity;
using Petronas.SampleApp.Repository.Interfaces;

namespace Petronas.SampleApp.Repository.Classes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext DbContext { get; set; }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(DbContext context)
        {
            DbContext = context;
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            // Save changes with the default options
            return DbContext.SaveChanges();
        }

        #region IUnitOfWork

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }

        #endregion
    }
}