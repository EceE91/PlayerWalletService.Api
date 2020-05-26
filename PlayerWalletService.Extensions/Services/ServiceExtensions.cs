using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Data;
using Microsoft.EntityFrameworkCore;
using PlayerWalletService.Data.Repositories;

namespace PlayerWalletService.Core.Services
{
    /// <summary>
    /// Class to use in Startup.cs
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// It injects the repositories in container.
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepositories(this IServiceCollection services)
        {
            //Scoped lifetime services are created once per request.
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
        }

        /// <summary>
        /// It injects the PlayerWalletContext in container.
        /// </summary>
        /// <param name="services"></param>
        public static void AddEF(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<PlayerWalletContext>(options =>
                {
                    //for tests
                    //options.UseInMemoryDatabase();
                  
                    options.UseSqlServer(Configuration.GetConnectionString("PlayerWalletDBConnectionString"));
                });
        }
    }
}
