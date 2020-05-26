using PlayerWalletService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerWalletService.Core.Interfaces.Repositories
{
    public interface IPlayerRepository : IRepositoryBase<Player>
    {
        /// <returns>Returns player and wallet info</returns>
        Task<Player> GetPlayerWalletInfoAsync(int playerId);
        Task<IEnumerable<Player>> GetAllPlayersWithWallet();
        Task<decimal> GetCurrentBalance(int playerId);
    }
}
