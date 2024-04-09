using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;

namespace AccountOwnerServer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();

            CreateMap<OwnerForCreationDto, Owner>();

            CreateMap<OwnerForUpdateDto, Owner>();

            CreateMap<Account, AccountDto>().ForMember(dest => dest.Owner, act => act.MapFrom(src => src.Owner.Name));

            CreateMap<AccountForCreationDto, Account>();

            CreateMap<AccountForUpdateDto, Account>();

            CreateMap<Transaction, TransactionDto>();

            CreateMap<TransactionForCreationDto, Transaction>();

            CreateMap<TransactionForUpdateDto, Transaction>();
        }
    }
}
