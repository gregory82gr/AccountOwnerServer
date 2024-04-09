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
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return FindAll()
                .OrderBy(ow => ow.Id)
                .ToList();
        }
        public Transaction GetTransactionById(Guid transactionId)
        {
            return FindByCondition(transaction => transaction.Id.Equals(transactionId)).Include("Account")
                 .FirstOrDefault();
        }

        public IEnumerable<Transaction> GetTransactionsByOwner(Guid ownerId)
        {
            return FindByCondition(a => a.Account.Owner.Id.Equals(ownerId)).ToList();
        }

        public void CreateTransaction(Transaction transaction) => Create(transaction);

        public void UpdateTransaction(Transaction transaction) => Update(transaction);

        public void DeleteTransaction(Transaction transaction) => Delete(transaction);
    }
}
