using PlayerWalletService.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerWalletService.Core.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<IEnumerable<Transaction>> GetAllByPlayerIdAsync(int playerId);
        Task AddNewTransactionAsync(Transaction entity);
    }
}
