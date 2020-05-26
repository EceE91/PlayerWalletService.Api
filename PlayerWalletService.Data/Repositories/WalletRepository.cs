using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Threading.Tasks;

namespace PlayerWalletService.Data.Repositories
{
    public class WalletRepository : RepositoryBase<Wallet>,IWalletRepository
    {
        public WalletRepository(PlayerWalletContext context,
                                    ILogger<WalletRepository> logger)
            : base(context, logger)
        {
        }     

        public async Task AddBalance(int walletId, decimal value)
        {
            var entity = await Query()
                            .FirstOrDefaultAsync(x => x.WalletId == walletId);
            if (entity == null)
                throw new RecordNotFoundException();

            var sql = "UPDATE Wallet SET " +
                "AvailableBalance = (AvailableBalance + {0}) WHERE WalletId = {1}";

            ExecuteQuery(sql, value, walletId);
        }

        public async Task SubtractBalance(int walletId, decimal value, decimal lossLimit)
        {
            var entity = await Query()
                            .FirstOrDefaultAsync(x => x.WalletId == walletId);

            if (entity == null)
                throw new RecordNotFoundException();

            var currentBalance = entity.AvailableBalance;

            if((currentBalance - value) <= lossLimit)
            {
                throw new LossLimitExceededException();
            }
            if(value > currentBalance)
            {
                throw new Exception($"Your current balance of {currentBalance} Euros is insufficient");
            }

            var sql = "UPDATE Wallet SET " +
               "AvailableBalance = (AvailableBalance - {0}) WHERE WalletId = {1}";

            ExecuteQuery(sql, value, walletId);
        }
    }
}
