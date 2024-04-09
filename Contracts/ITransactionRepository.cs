using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITransactionRepository : IRepositoryBase<Transaction> 
    {
        IEnumerable<Transaction>? GetAllTransactions();
        Transaction GetTransactionById(Guid transactionId);
        IEnumerable<Transaction> GetTransactionsByOwner(Guid ownerId);
        void CreateTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(Transaction transaction);
    }
}
