using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PlayerWalletService.Api.Controllers;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using PlayerWalletService.Controllers;

namespace PlayerWalletService.Api.XUnitTests
{
    public class TransactionControllerTests
    {
        [Fact]
        public void GetCurrentBalanceReturns200()
        {
            // Arrange
            var mockRepo = new Mock<IPlayerRepository>();
            var mockLogger = new Mock<ILogger<Player>>();
            var mockMapper = new Mock<IMapper>();

            var playerId = 1;
            mockRepo.Setup(repo => repo.GetCurrentBalance(playerId))
                .Returns(Task<decimal>.FromResult(300M));
            var controller = new PlayerController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = controller.GetCurrentBalance(playerId).Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public void TransactionsByPlayerIdReturns200()
        {
            // Arrange
            var mockRepo = new Mock<ITransactionRepository>();
            var mockLogger = new Mock<ILogger<Transaction>>();
            var mockMapper = new Mock<IMapper>();

            var playerId = 1;
            mockRepo.Setup(repo => repo.GetAllByPlayerIdAsync(playerId))
                .Returns(GetTestTransactions());
            var controller = new TransactionController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = controller.Get(playerId).Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void AllTransactionsReturns200()
        {
            // Arrange
            var mockRepo = new Mock<ITransactionRepository>();
            var mockLogger = new Mock<ILogger<Transaction>>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllTransactions())
                .Returns(GetTestTransactions());
            var controller = new TransactionController(mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = controller.Get().Result as OkObjectResult;

            Assert.Equal(200, result.StatusCode);
        }

        private TransactionsInfoViewModel GetNewTransactionInfo()
        {
            var transaction = new TransactionsInfoViewModel()
            {
                TransactionId = 1,
                TransactionAmount = 2000,
                TransactionDate = DateTime.Now,
                Type = TransactionType.PAY,
                PlayerId = 1,
                PlayerName = "test player",
                PlayerEmail = "testplayer@test.com",
                PlayerLossLimit = 300,
                PlayersBalance = 800
            };
            return transaction;
        }

        private async Task<IEnumerable<Transaction>> GetTestTransactions()
        {
            var results = new List<Transaction>() {
                    new Transaction()
                    {
                        TransactionId =1,
                        TransactionAmount=600,
                        TransactionDate=DateTime.Now,
                        Type= TransactionType.PAY,
                        Player= new Player()
                        {
                            PlayerId=1,
                            Name="ece",
                            Email="eceercan91@gmail.com",
                            LossLimit=200,
                            Wallet = new Wallet()
                            {
                                WalletId=1,
                                AvailableBalance=0
                            }
                        }
                    },
                    new Transaction()
                    {
                        TransactionId =2,
                        TransactionAmount=3700,
                        TransactionDate=DateTime.Now,
                        Type= TransactionType.WIN,
                        Player= new Player()
                        {
                            PlayerId=2,
                            Name="test",
                            Email="test@gmail.com",
                            Wallet = new Wallet()
                            {
                                WalletId=2,
                                AvailableBalance=6000
                            }
                        }
                    }
            };

            return await Task.FromResult<IEnumerable<Transaction>>(results);
        }

    }
}
