using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Api.Controllers;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Exceptions;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlayerWalletService.Controllers
{
    /// <summary>
    /// player and wallet controller
    /// </summary>
    [Route("api")]
    [ProducesResponseType(typeof(ApiResult<string>), 401)]
    [ProducesResponseType(typeof(ApiResult<string>), 400)]
    [ProducesResponseType(typeof(ApiResult<string>), 404)]
    [ProducesResponseType(typeof(ApiResult<string>), 200)]
    public class PlayerController : ControllerBase<Player>
    {
        public PlayerController(IPlayerRepository playerRepo,
                                    ILogger<Player> logger,
                                    IMapper mapper)
            : base(playerRepo, logger, mapper)
        {
        }

        /// <summary>
        /// list all players 
        /// </summary>
        [HttpGet("players")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<PlayerViewModel>>), 200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var list = await (_repository as IPlayerRepository).GetAllPlayersWithWallet();

                return ReturnOk(_mapper.Map<IEnumerable<Player>, IEnumerable<PlayerWalletViewModel>>(list));
            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message);
            }
        }


        /// <summary>
        /// list all players 
        /// </summary>
        [HttpGet("player/{playerId:int}/balance")]
        [ProducesResponseType(typeof(ApiResult<PlayerViewModel>), 200)]
        public async Task<IActionResult> GetCurrentBalance(int playerId)
        {
            try
            {
                var entity = await (_repository as IPlayerRepository).GetPlayerWalletInfoAsync(playerId);

                return ReturnOk(_mapper.Map<Player, PlayerWalletViewModel>(entity));
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

    }
 }
