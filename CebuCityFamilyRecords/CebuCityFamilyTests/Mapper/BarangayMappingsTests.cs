using AutoMapper;
using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Mappings;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPITests.Mapper
{
    public class BarangayMappingsTests
    {
        private readonly IMapper _mapper;

        public BarangayMappingsTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BarangayMappings>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void CreateMap_BarangayCreationDtoToBarangay_MappingIsValid()
        {
            // Arrange
            var barangayCreationDto = new BarangayCreationDto
            {
                Name = "Test Barangay",
                Captain = "Test Captain"
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<BarangayCreationDto, Barangay>(barangayCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barangayCreationDto.Name, result.Name);
            Assert.Equal(barangayCreationDto.Captain, result.Captain);
        }

        [Fact]
        public void CreateMap_BarangayToBarangayDto_MappingIsValid()
        {
            // Arrange
            var barangay = new Barangay
            {
                Name = "Test Barangay",
                Captain = "Test Captain"
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<Barangay, BarangayDto>(barangay);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barangay.Name, result.Name);
            Assert.Equal(barangay.Captain, result.Captain);
        }

        [Fact]
        public void CreateMap_BarangayUpdationDtoToBarangay_MappingIsValid()
        {
            // Arrange
            var barangayUpdationDto = new BarangayUpdationDto
            {
                Name = "Updated Barangay",
                Captain = "Updated Captain"
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<BarangayUpdationDto, Barangay>(barangayUpdationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barangayUpdationDto.Name, result.Name);
            Assert.Equal(barangayUpdationDto.Captain, result.Captain);
        }
    }
}
