using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Interfaces.Repositories;
using PlayerWalletService.Core.Models;
using System.Net;

namespace PlayerWalletService.Api.Controllers
{
    public class ControllerBase<T> : Controller
       where T : ModelBase
    {
        protected readonly IRepositoryBase<T> _repository;
        protected readonly ILogger _logger;
        protected readonly IMapper _mapper;

        public ControllerBase(IRepositoryBase<T> repository,
                                ILogger logger,
                                IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <returns>Returns success 200</returns>
        [NonAction]
        protected IActionResult ReturnOk<TData>(TData data)
        {
            var result = new ApiResult<TData>
            {
                Done = true,
                Data = data,
                Code = HttpStatusCode.OK
            };

            return Ok(result);
        }

        /// <returns>Returns error 400</returns>
        [NonAction]
        protected IActionResult ReturnError<TData>(TData data)
        {
            var result = new ApiResult<TData>
            {
                Done = false,
                Data = data,
                Code = HttpStatusCode.BadRequest
            };

            return BadRequest(result);
        }

        /// <returns>Returns not found 404</returns>
        [NonAction]
        protected IActionResult ReturnNotFound<TData>(TData data)
        {
            var result = new ApiResult<TData>
            {
                Done = false,
                Data = data,
                Code = HttpStatusCode.NotFound
            };

            return NotFound(result);
        }

        /// <returns>Return anauthorized 401</returns>
        [NonAction]
        protected IActionResult ReturnUnauthorized()
        {
            return new StatusCodeResult((int)HttpStatusCode.Unauthorized);
        }
    }
}
