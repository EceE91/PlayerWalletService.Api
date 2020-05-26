using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerWalletService.Data.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>,IPlayerRepository
    {
        public PlayerRepository(PlayerWalletContext context,
                                    ILogger<PlayerRepository> logger)
            : base(context, logger)
        {
        }

        /// <summary>
        /// Gets all player's current balance info
        /// </summary>
        public async Task<decimal> GetCurrentBalance(int playerId)
        {
            var entity = await Query().Where(x => x.PlayerId == playerId)
                           .Include(x => x.Wallet).SingleOrDefaultAsync();

            if (entity == null)
                throw new RecordNotFoundException();

            return entity.Wallet.AvailableBalance;
        }

        /// <summary>
        /// Gets all players with balance info
        /// </summary>
        /// <returns>Returns all players list.</returns>
        public async Task<IEnumerable<Player>> GetAllPlayersWithWallet()
        {      
                var list = await Query()
                               .Include(x => x.Wallet).ToListAsync();

                if (!list.Any())
                    throw new RecordNotFoundException();

                return list;            
        }
     

        /// <returns>Returns player and wallet from wallet</returns>
        public async Task<Player> GetPlayerWalletInfoAsync(int playerId)
        {
            var entity = await Query()
                            .Include(x => x.Wallet)
                            .FirstOrDefaultAsync(x => x.PlayerId == playerId);

            if (entity == null)
                throw new RecordNotFoundException();

            return entity;
        }
    }
}
