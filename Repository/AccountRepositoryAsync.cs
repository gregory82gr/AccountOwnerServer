using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepositoryAsync : RepositoryBase<Account>, IAccountRepositoryAsync
    {
        public AccountRepositoryAsync(RepositoryContext repositoryContext)
       : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()=>
            await FindAll().Include("Owner")
                .OrderByDescending(ow => ow.DateCreated).ThenBy(ow => ow.OwnerId)
                .ToListAsync();
       
        public async Task<Account> GetAccountByIdAsync(Guid accountId) =>
            await FindByCondition(account => account.Id.Equals(accountId)).Include("Owner")
                .FirstOrDefaultAsync();
        
        public async Task<IEnumerable<Account>> AccountsByOwnerAsync(Guid ownerId) =>
            await FindByCondition(a => a.OwnerId.Equals(ownerId)).ToListAsync();

        public void CreateAccount(Account account) => Create(account);

        public void UpdateAccount(Account account) => Update(account);

        public void DeleteAccount(Account account) => Delete(account);
    }
}
