using PlayerWalletService.Core.Models;
using System;

namespace PlayerWalletService.Api.Models
{
    public class TransactionsInfoViewModel
    {
        public int TransactionId { get; set; }
        public decimal TransactionAmount { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerEmail { get; set; }
        public decimal PlayerLossLimit { get; set; }
        public decimal PlayersBalance { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; } = TransactionType.PAY;
    }
}
