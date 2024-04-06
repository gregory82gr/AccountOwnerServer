using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapperAsync
    {
        IOwnerRepositoryAsync Owner { get; }
        IAccountRepositoryAsync Account { get; }
        IUserInfoRepository UserInfo { get; }
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
