using System.ComponentModel.DataAnnotations;

namespace PlayerWalletService.Api.Models
{
    public class PlayerViewModel
    {
        public int PlayerId { get; set; }

        [Required,MaxLength(20)]
        public string Name { get; set; }

        [Required,MaxLength(100)]
        public string Email { get; set; }

        public int WalletId { get; set; }

        public decimal LossLimit { get; set; }
    }
}
