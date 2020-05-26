using AutoMapper;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Models;

namespace PlayerWalletService.Api.Mapper.Resolvers
{
    /// <summary>
    /// Custom type resolver for AutoMapper
    /// It converts user info(transaction, player) into VMs.
    /// </summary>
    public class TransactionInfoResover : ITypeConverter<Transaction, TransactionsInfoViewModel>
    {
        public TransactionsInfoViewModel Convert(Transaction source, TransactionsInfoViewModel destination, ResolutionContext context)
        {
            var info = new TransactionsInfoViewModel
            {
                TransactionId = source.TransactionId,
                PlayerId = source.PlayerId,
                PlayerName = source.Player.Name,
                PlayerEmail = source.Player.Email,
                PlayerLossLimit = source.Player.LossLimit,
                PlayersBalance = source.Player.Wallet.AvailableBalance,
                TransactionAmount = source.TransactionAmount,
                TransactionDate = source.TransactionDate,
                Type = source.Type
            };
            return info;
        }
    }

}
