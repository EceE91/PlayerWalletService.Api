using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerWalletService.Core.Models
{
    /// <summary>
    /// Model for Transaction entity
    /// </summary>
    public class Transaction : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int PlayerId { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        public TransactionType Type { get; set; } = TransactionType.PAY;
    }

    public enum TransactionType
    {
        PAY = 1,
        BET = 2,
        WIN = 3
    }
}