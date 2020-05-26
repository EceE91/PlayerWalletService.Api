using Microsoft.EntityFrameworkCore;
using PlayerWalletService.Core.Models;

namespace PlayerWalletService.Data
{
    /// <summary>
    /// Context class for EntityFramewirk
    /// </summary>
    public class PlayerWalletContext : DbContext
    {
        /// <summary>
        /// Player Set
        /// </summary>
        public DbSet<Player> Players { get; set; }

        /// <summary>
        /// Wallet set
        /// </summary>
        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public PlayerWalletContext(DbContextOptions<PlayerWalletContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Id columns on database should be auto-incerement identity
            modelBuilder.Entity<Player>()
            .Property(f => f.PlayerId)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Wallet>()
           .Property(f => f.WalletId)
           .ValueGeneratedOnAdd();

            modelBuilder.Entity<Transaction>()
           .Property(f => f.TransactionId)
           .ValueGeneratedOnAdd();
        }
    }
}
