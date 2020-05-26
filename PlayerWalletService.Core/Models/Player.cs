using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerWalletService.Core.Models
{
    /// <summary>
    /// Model for Player entity
    /// </summary>
    public class Player :ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required,MaxLength(20)]
        public string Name { get; set; }

        [Required,MaxLength(100)]
        public string Email { get; set; }

        public decimal LossLimit { get; set; }

        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }

        public int WalletId { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}