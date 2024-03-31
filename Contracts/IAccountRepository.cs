using Entities.Models;

namespace Contracts
{
    public interface IAccountRepository:IRepositoryBase<Account>
    {
        IEnumerable<Account> GetAllAccounts();
        Account GetAccountById(Guid accountId);
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(Account account);

    }
}
