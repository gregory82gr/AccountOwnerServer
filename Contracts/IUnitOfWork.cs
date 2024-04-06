using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
        void Rollback();
        IRepositoryWrapperAsync RepositoryWrapper { get; }
    }
}
