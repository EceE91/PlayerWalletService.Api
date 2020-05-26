using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerWalletService.Core.Models
{
    /// <summary>
    /// Model for Wallet entity
    /// </summary>
    [Table("Wallet")]
    public class Wallet : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WalletId { get; set; }
        public decimal AvailableBalance { get; set; }
    }
}