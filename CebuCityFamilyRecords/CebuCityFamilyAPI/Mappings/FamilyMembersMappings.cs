using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Mappings
{
    public class FamilyMembersMappings : Profile
    {
        public FamilyMembersMappings()
        {
            CreateMap<FamilyMemberWithDetails, FamilyMemberWithDetailsDto>();
            CreateMap<FamilyMemberWithDetailsDto, FamilyMemberWithDetails>();
            CreateMap<FamilyMemberWithDetailsCreationDto, FamilyMemberWithDetails>();
            CreateMap<FamilyMemberWithDetails, FamilyMemberWithDetailsUpdationDto>();
            CreateMap<FamilyMemberWithDetailsUpdationDto, FamilyMemberWithDetails>();
        }
    }
}
