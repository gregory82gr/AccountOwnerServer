using System;

namespace Entities.DataTransferObjects
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string? AccountType { get; set; }
        public Guid OwnerId { get; set; }
        public string? Owner { get; set; }

        public IEnumerable<TransactionDto>? Transactions { get; set; }
    }
}
