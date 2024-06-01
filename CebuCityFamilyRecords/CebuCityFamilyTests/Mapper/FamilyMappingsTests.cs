using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Mappings;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPITests.Mapper
{
    public class FamilyMappingsTests
    {
        private readonly IMapper _mapper;

        public FamilyMappingsTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FamilyMappings>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void CreateMap_FamilyCreationDtoToFamily_MappingIsValid()
        {
            // Arrange
            var familyCreationDto = new FamilyCreationDto
            {
                Name = "Test Family",
                Sitio = "Test Sitio"
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<FamilyCreationDto, Family>(familyCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familyCreationDto.Name, result.Name);
            Assert.Equal(familyCreationDto.Sitio, result.Sitio);
        }

        [Fact]
        public void CreateMap_FamilyToFamilyDto_MappingIsValid()
        {
            // Arrange
            var family = new Family
            {
                Id = 1,
                Name = "Test Family",
                Sitio = "Test Sitio",
                Barangay = new Barangay { },
                BId = 1,
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<Family, FamilyDto>(family);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(family.Id, result.Id);
            Assert.Equal(family.Name, result.Name);
            Assert.Equal(family.Sitio, result.Sitio);
        }

        [Fact]
        public void CreateMap_FamilyUpdationDtoToFamily_MappingIsValid()
        {
            // Arrange
            var familyUpdationDto = new FamilyUpdationDto
            {
                Sitio = "Updated Sitio",
                // Add any other necessary properties
            };

            // Act
            var result = _mapper.Map<FamilyUpdationDto, Family>(familyUpdationDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familyUpdationDto.Sitio, result.Sitio);
        }

        [Fact]
        public void CreateMap_FamilyWithMemberToFamilyWithMemberDto_MappingIsValid()
        {
            // Arrange
            var familyWithMember = new FamilyWithMember
            {
                Id = 1,
                Name = "Test Family",
                Sitio = "Test Sitio",
                BId = 2,
                Barangay = new Barangay { /* Set any necessary properties */ },
                FamilyMembers = new List<FamilyMemberWithDetails>
                {
                    new FamilyMemberWithDetails {},
                    new FamilyMemberWithDetails {}
                }
            };

            // Act
            var result = _mapper.Map<FamilyWithMember, FamilyWithMemberDto>(familyWithMember);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familyWithMember.Id, result.Id);
            Assert.Equal(familyWithMember.Name, result.Name);
            Assert.Equal(familyWithMember.Sitio, result.Sitio);
            Assert.NotNull(result.FamilyMembers);
            Assert.Equal(familyWithMember.FamilyMembers.Count, result.FamilyMembers.Count);
        }

        [Fact]
        public void CreateMap_FamilyMemberWithDetailsToFamilyMemberWithDetailsDto_MappingIsValid()
        {
            // Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetails
            {
                Id = 1,
                LastName = "string",
                FirstName = "string",
                Age = 150,
                MaritalStatus = "string",
                BirthDate = "string",
                Gender = "string",
                Occupation = "string",
                ContactNo = "09988888888",
                Religion = "string"
            };

            // Act
            var result = _mapper.Map<FamilyMemberWithDetails, FamilyMemberWithDetailsDto>(familyMemberWithDetails);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familyMemberWithDetails.Id, result.Id);
            Assert.Equal(familyMemberWithDetails.LastName, result.LastName);
            Assert.Equal(familyMemberWithDetails.FirstName, result.FirstName);
            Assert.Equal(familyMemberWithDetails.Age, result.Age);
            Assert.Equal(familyMemberWithDetails.MaritalStatus, result.MaritalStatus);
            Assert.Equal(familyMemberWithDetails.BirthDate, result.BirthDate);
            Assert.Equal(familyMemberWithDetails.Gender, result.Gender);
            Assert.Equal(familyMemberWithDetails.Occupation, result.Occupation);
            Assert.Equal(familyMemberWithDetails.ContactNo, result.ContactNo);
            Assert.Equal(familyMemberWithDetails.Religion, result.Religion);
        }
    }
}
