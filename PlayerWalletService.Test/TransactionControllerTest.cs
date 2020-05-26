using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlayerWalletService.Api.Controllers;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PlayerWalletService.Test
{
    public class TransactionControllerTest
    {
        [Fact]
        public void ReturnsAllTransactions()
        {
            // Arrange
            var mockRepo = new Mock<ITransactionRepository>();
            var mockLogger = new Mock<ILogger<Transaction>>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllTransactions())
                .Returns(GetTestTransactions());
            var controller = new TransactionController( mockRepo.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = controller.Get();

            // Assert
            //var res = Assert.IsType<ActionResult<IEnumerable<IProduct>>>(result);
           // var model = Assert.IsAssignableFrom<List<IProduct>>(res.Value);


            //Assert.Equal(2, model.Count);
        }


        private async Task<IEnumerable<Transaction>> GetTestTransactions()
        {
            var results = new List<Transaction>() {
                    new Transaction()
                    {
                        TransactionId =1,
                        TransactionAmount=600,
                        TransactionDate=DateTime.Now,
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