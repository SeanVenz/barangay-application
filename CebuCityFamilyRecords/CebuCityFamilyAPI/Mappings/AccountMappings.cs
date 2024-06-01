using AutoMapper;
using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Mappings
{
    public class AccountMappings : Profile
    {
        public AccountMappings()
        {
            CreateMap<AccountCreationDto, Account>();
            CreateMap<Account, AccountDto>();
        }
    }
}
