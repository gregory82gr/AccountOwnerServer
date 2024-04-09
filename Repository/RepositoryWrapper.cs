using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private RepositoryContext _repoContext; 
        private IOwnerRepository _owner; 
        private IAccountRepository _account;
        private ITransactionRepository _transaction;
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

        public ITransactionRepository Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new TransactionRepository(_repoContext);
                }
                return _transaction;
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
