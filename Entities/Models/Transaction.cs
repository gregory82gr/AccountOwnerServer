using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("transaction")]
    public class Transaction
    {

        [Column("TransactionId")]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Transaction Date is required")]
        public DateTime TransactionDateTime { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        [ForeignKey(nameof(Account))]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "AccountTo is required")]
        public Guid AccountTo { get; set; }

        [Required(ErrorMessage = "TransactionAmount type is required")]
        public decimal TransactionAmount { get; set; }
        public Account Account { get; set; }

    }
}
