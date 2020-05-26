using AutoMapper;
using PlayerWalletService.Api.Mapper.Resolvers;
using PlayerWalletService.Api.Models;
using PlayerWalletService.Core.Models;

namespace PlayerWalletService.Api.Mapper.MapperProfiles
{
    /// <summary>
    /// A mapper configuration for AutoMaper
    /// </summary>
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            //Transaction to TransactionsInfoViewModel, and vice-versa
            CreateMap<Transaction, TransactionsInfoViewModel>().ConvertUsing(new TransactionInfoResover());

            //Player to PlayerViewModel, and vice-versa
            CreateMap<Player, PlayerViewModel>().ReverseMap();

            //Wallet to WalletViewModel, and vice-versa
            CreateMap<Wallet, WalletViewModel>().ReverseMap();

            //Transaction to TransactionViewModel, and vice-versa
            CreateMap<Transaction, TransactionViewModel>().ReverseMap();
        }
    }
}
