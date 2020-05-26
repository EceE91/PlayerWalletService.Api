using Microsoft.EntityFrameworkCore.Internal;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerWalletService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PlayerWalletContext context)
        {
            //Creating the db
            context.Database.EnsureCreated();

            // DB has been seeded
            if (context.Players.Any())
                return;

            // init seed data
            var transactions = new List<Transaction>()
            {
                new Transaction(){
                    TransactionAmount=5000,
                    Type = TransactionType.PAY,
                    TransactionDate= DateTime.Now,
                    Player= new Player()
                    {
                        Name="Ece ERCAN",
                        Email="eceercan91@hotmail.com",
                        LossLimit=3700,
                        Wallet = new Wallet()
                        {
                            AvailableBalance=0
                        }
                    }
                },
                new Transaction(){
                    TransactionAmount=100,
                    Type = TransactionType.BET,
                    TransactionDate= DateTime.Now,
                    Player = new Player()
                    {
                        Name="Matt Spiteri",
                        Email="matt@gmail.com",
                        LossLimit=200,
                        Wallet = new Wallet()
                        {
                            AvailableBalance=2000
                        }
                    }
                },
                new Transaction(){
                    TransactionAmount=400,
                    Type = TransactionType.WIN,
                    TransactionDate= DateTime.Now,
                    Player = new Player()
                    {
                        Name="Sarah Johnson",
                        Email="SarahJohnson@gmail.com",
                        LossLimit=6200,
                        Wallet = new Wallet()
                        {
                            AvailableBalance=8600
                        }
                    }
                }
            };

            context.Transactions.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
