using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private RepositoryContext _repoContext; 
        private IOwnerRepository _owner; 
        private IAccountRepository _account;
        private IUserInfoRepository _userInfo;
        public IOwnerRepository Owner 
        { 
            get 
            { 
                if (_owner == null) 
                { 
                    _owner = new OwnerRepository(_repoContext); 
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

        public RepositoryWrapper(RepositoryContext repositoryContext) 
        { 
            _repoContext = repositoryContext; 
        } 
        
        public void Save() 
        {
            _repoContext.SaveChanges();
        } 
    }
}
