using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public Guid AccountId { get; set; }
        public Guid AccountTo { get; set; }
        public decimal  TransactionAmount { get; set; }
    }
}
