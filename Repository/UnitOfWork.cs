using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepositoryWrapperAsync _repository;
        RepositoryContext _repositoryContext;

        public UnitOfWork(RepositoryContext repositoryContext, IRepositoryWrapperAsync repository)
        {
            this._repositoryContext = repositoryContext;
            this._repository = repository;
        }

        public IRepositoryWrapperAsync RepositoryWrapper { get { return this._repository; } }

        public bool Commit()
        {
            /*this._repositoryContext.SaveChanges()*/;
            bool returnValue = true;
            using (var dbContextTransaction = _repositoryContext.Database.BeginTransaction())
            {
                try
                {
                    _repositoryContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    dbContextTransaction.Rollback();
                }
            }
            return returnValue;

        }
        public void Rollback()
        {
            // Rollback changes if needed
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._repositoryContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
