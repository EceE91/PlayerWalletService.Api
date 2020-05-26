using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerWalletService.Data.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>,
                                                ITransactionRepository
    {
        private readonly IWalletRepository _walletRepo;
        private readonly IPlayerRepository _playerRepo;
        public TransactionRepository(PlayerWalletContext context,
                                            ILogger<TransactionRepository> logger,
                                            IWalletRepository walletRepo,
                                            IPlayerRepository playerRepo)
            : base(context, logger)
        {
            _walletRepo = walletRepo;
            _playerRepo = playerRepo;
        }

        public override void BeforeAdd(Transaction entity)
        {
            entity.TransactionDate = DateTime.UtcNow;

            ApplyCommomValidations(entity);

            base.BeforeAdd(entity);
        }

        private void ApplyCommomValidations(Transaction entity)
        {
            //The value must be greater than 0
            if (entity.TransactionAmount == 0)
                throw new Exception("Amount must be greater than 0!");
        }


        /// <summary>
        /// Gets all transactions
        /// </summary>
        /// <returns>Returns all transaction list.</returns>
        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            var list = await Query()
                           .Include(x => x.Player)
                               .ThenInclude(t => t.Wallet).ToListAsync();

            if (!list.Any())
                throw new RecordNotFoundException();

            return list;
        }

        /// <summary>
        /// Gets all transactions by player
        /// </summary>
        /// <returns>Returns a transaction list.</returns>
        public async Task<IEnumerable<Transaction>> GetAllByPlayerIdAsync(int playerId)
        {
            return await Query().Where(x => x.PlayerId == playerId).OrderBy(x=> x.TransactionDate).ToListAsync();
        }

        /// <summary>
        /// Adds a new transaction pay/bet/win
        /// </summary>
        public async Task AddNewTransactionAsync(Transaction entity)
        {
            //Get player info
            var playerEntity = await _playerRepo.GetAsync(entity.PlayerId);

            dataContext.Database.BeginTransaction();
            try
            {
                if (entity.Type == TransactionType.PAY || entity.Type == TransactionType.WIN)
                {
                    // add amount to balance
                    await _walletRepo.AddBalance(playerEntity.WalletId, entity.TransactionAmount);
                }
                else
                {
                    // subtract amount from balance
                    await _walletRepo.SubtractBalance(playerEntity.WalletId, entity.TransactionAmount, playerEntity.LossLimit);
                }
              
                await AddAsync(entity);
                dataContext.Database.CommitTransaction();
            }
            catch
            {
                dataContext.Database.RollbackTransaction();
                throw;
            }
        }
    }
}
