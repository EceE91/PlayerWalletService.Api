using PlayerWalletService.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PlayerWalletService.Api.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [Required]
        public int PlayerId { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public TransactionType Type { get; set; } = TransactionType.PAY;
    }
}
