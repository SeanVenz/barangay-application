using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Mappings
{
    public class FamilyMappings : Profile
    {
        public FamilyMappings()
        {
            CreateMap<FamilyCreationDto, Family>();
            CreateMap<Family, FamilyDto>();
            CreateMap<FamilyUpdationDto, Family>();
            CreateMap<FamilyWithMember, FamilyWithMemberDto>();
            CreateMap<FamilyMemberWithDetails, FamilyMemberWithDetailsDto>();
        }
    }
}
