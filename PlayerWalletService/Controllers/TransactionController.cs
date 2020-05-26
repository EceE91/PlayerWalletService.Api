using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Api.Filters;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using PlayerWalletService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerWalletService.Api.Controllers
{
    /// <summary>
    /// transactions controller
    /// </summary>
    [Route("api/transactions")]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 400)]
    [ProducesResponseType(typeof(ApiResult<string>), 404)]
    [ProducesResponseType(typeof(ApiResult<string>), 200)]
    public class TransactionController : ControllerBase<Transaction>
    {
        public TransactionController(ITransactionRepository repo,
                                    ILogger<Transaction> logger,
                                    IMapper mapper)
            : base(repo, logger, mapper)
        {
        }

        /// <summary>
        /// Gets a specific player's transactions 
        /// </summary>
        [HttpGet("{playerId:int}/transactions")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<TransactionViewModel>>), 200)]
        public async Task<IActionResult> Get(int playerId)
        {
            try
            {
                var list = await (_repository as ITransactionRepository).GetAllByPlayerIdAsync(playerId);

                return ReturnOk(_mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(list));
            }
            catch (RecordNotFoundException ex)
            {
                return ReturnNotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }

        /// <summary>
        /// Gets a all players' transactions 
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<TransactionsInfoViewModel>>), 200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await (_repository as ITransactionRepository).GetAllTransactions();

                return ReturnOk(_mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionsInfoViewModel>>(list));
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new transaction.
        /// </summary>
        /// <returns>Returns Ok/Error.</returns>
        [HttpPost("Pay")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResult<string>), 200)]
        public async Task<IActionResult> Pay([FromBody]TransactionViewModel vm)
        {
            try
            {
                vm.Type = TransactionType.PAY;
                var entity = _mapper.Map<TransactionViewModel, Transaction>(vm);

                await (_repository as TransactionRepository).AddNewTransactionAsync(entity);

                return ReturnOk("Successful");
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new transaction.
        /// </summary>
        /// <returns>Returns Ok/Error.</returns>
        [HttpPost("Win")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResult<string>), 200)]
        public async Task<IActionResult> Win([FromBody]TransactionViewModel vm)
        {
            try
            {
                vm.Type = TransactionType.WIN;
                var entity = _mapper.Map<TransactionViewModel, Transaction>(vm);

                await (_repository as TransactionRepository).AddNewTransactionAsync(entity);

                return ReturnOk("Successful");
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }

        /// <summary>
        /// Adds a new bet transaction.
        /// </summary>
        /// <returns>Returns Ok/Error.</returns>
        [HttpPost("Bet")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResult<string>), 200)]
        public async Task<IActionResult> Bet([FromBody]TransactionViewModel vm)
        {
            try
            {
                vm.Type = TransactionType.BET;
                var entity = _mapper.Map<TransactionViewModel, Transaction>(vm);

                await (_repository as TransactionRepository).AddNewTransactionAsync(entity);

                return ReturnOk("Successful");
            }
            catch (ThereIsNotEnoughBalanceException ex)
            {
                return ReturnError(ex.Message);
            }
            catch (LossLimitExceededException ex)
            {
                return ReturnError(ex.Message);
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }
    }
}
