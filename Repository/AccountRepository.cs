using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository 
    { 
        public AccountRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext) 
        { 
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll().Include("Owner")
                .OrderByDescending(ow => ow.DateCreated).ThenBy(ow => ow.OwnerId)
                .ToList();
        }
        public Account GetAccountById(Guid accountId)
        {
            return FindByCondition(account => account.Id.Equals(accountId)).Include("Owner")
                .FirstOrDefault();
        }
        public IEnumerable<Account> AccountsByOwner(Guid ownerId) =>
            FindByCondition(a => a.OwnerId.Equals(ownerId)).ToList();

        public void CreateAccount(Account account) => Create(account);

        public void UpdateAccount(Account account) => Update(account);

        public void DeleteAccount(Account account) => Delete(account);
    }
}
