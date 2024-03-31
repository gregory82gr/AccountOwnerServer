using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryWrapperAsync : IRepositoryWrapperAsync 
    {
        private RepositoryContext _repoContext;
        private IOwnerRepositoryAsync _owner;
        private IAccountRepository _account;
        private IUserInfoRepository _userInfo;
        public IOwnerRepositoryAsync Owner
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new OwnerRepositoryAsync(_repoContext);
                }
                return _owner;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext);
                }
                return _account;
            }
        }

        public IUserInfoRepository UserInfo
        {
            get
            {
                if (_userInfo == null)
                {
                    _userInfo = new UserInfoRepository(_repoContext);
                }
                return _userInfo;
            }
        }

        public RepositoryWrapperAsync(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _repoContext.SaveChangesAsync(cancellationToken);
        }
    }
}
