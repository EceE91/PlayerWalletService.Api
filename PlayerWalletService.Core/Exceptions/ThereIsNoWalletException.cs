using System;

namespace PlayerWalletService.Core.Exceptions
{
    public class ThereIsNotEnoughBalanceException : Exception
        {
            public ThereIsNotEnoughBalanceException() : base("Current Balance insufficient.") { }
        }
}
