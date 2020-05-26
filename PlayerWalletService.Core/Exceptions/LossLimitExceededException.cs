using System;

namespace PlayerWalletService.Core.Exceptions
{
    public class LossLimitExceededException : Exception
    {
        public LossLimitExceededException() : base("You exceeded the loss limit.") { }
    }
}
