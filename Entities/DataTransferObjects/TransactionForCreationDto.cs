using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TransactionForCreationDto
    {
       
        [Required(ErrorMessage = "Transaction Date is required")]
        public DateTime TransactionDateTime { get; set; }

        [Required(ErrorMessage = "AccountId is required")]
        public Guid AccountId { get; set; }

        [Required(ErrorMessage = "AccountTo is required")]
        public Guid AccountTo { get; set; }

        [Required(ErrorMessage = "TransactionAmount type is required")]
        public decimal TransactionAmount { get; set; }
    }
}
