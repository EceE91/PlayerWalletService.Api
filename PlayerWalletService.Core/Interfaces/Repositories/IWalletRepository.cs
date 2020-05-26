using PlayerWalletService.Core.Models;
using System.Threading.Tasks;

namespace PlayerWalletService.Core.Interfaces.Repositories
{
    public interface IWalletRepository :IRepositoryBase<Wallet>
    {
        Task AddBalance(int walletId, decimal value);
        Task SubtractBalance(int walletId, decimal value, decimal lossLimit);
    }
}
