using AutoMapper;
using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPI.Mappings
{
    public class BarangayMappings : Profile
    {
        public BarangayMappings()
        {
            CreateMap<BarangayCreationDto, Barangay>();
            CreateMap<Barangay, BarangayDto>();
            CreateMap<BarangayUpdationDto, Barangay>();
        }
    }
}
