using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Mappings;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPITests.Mapper
{
    public class FamilyMembersMappingsTest
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public FamilyMembersMappingsTest()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FamilyMembersMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void FamilyMemberWithDetailsMapToFamilyMemberWithDetailsDto_MapsCorrectly_ReturnsMappedAccount()
        {
            //Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetails
            {
                Id = 1,
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic",
                FId = 1
            };

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetailsDto>(familyMemberWithDetails);

            //Assert
            Assert.Equal(familyMember.Id, familyMemberWithDetails.Id);
            Assert.Equal(familyMember.LastName, familyMemberWithDetails.LastName);
            Assert.Equal(familyMember.FirstName, familyMemberWithDetails.FirstName);
            Assert.Equal(familyMember.Age, familyMemberWithDetails.Age);
            Assert.Equal(familyMember.MaritalStatus, familyMemberWithDetails.MaritalStatus);
            Assert.Equal(familyMember.BirthDate, familyMemberWithDetails.BirthDate);
            Assert.Equal(familyMember.Gender, familyMemberWithDetails.Gender);
            Assert.Equal(familyMember.Occupation, familyMemberWithDetails.Occupation);
            Assert.Equal(familyMember.ContactNo, familyMemberWithDetails.ContactNo);
            Assert.Equal(familyMember.Religion, familyMemberWithDetails.Religion);
        }

        [Fact]
        public void FamilyMemberWithDetailsMapToFamilyMemberWithDetailsDto_NullInput_ReturnsNull()
        {
            //Arrange
            FamilyMemberWithDetails familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetailsDto>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }

        [Fact]
        public void FamilyMemberWithDetailsDtoMapToFamilyMemberWithDetails_MapsCorrectly_ReturnsMappedAccount()
        {
            //Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetailsDto
            {
                Id = 1,
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Equal(familyMember.Id, familyMemberWithDetails.Id);
            Assert.Equal(familyMember.LastName, familyMemberWithDetails.LastName);
            Assert.Equal(familyMember.FirstName, familyMemberWithDetails.FirstName);
            Assert.Equal(familyMember.Age, familyMemberWithDetails.Age);
            Assert.Equal(familyMember.MaritalStatus, familyMemberWithDetails.MaritalStatus);
            Assert.Equal(familyMember.BirthDate, familyMemberWithDetails.BirthDate);
            Assert.Equal(familyMember.Gender, familyMemberWithDetails.Gender);
            Assert.Equal(familyMember.Occupation, familyMemberWithDetails.Occupation);
            Assert.Equal(familyMember.ContactNo, familyMemberWithDetails.ContactNo);
            Assert.Equal(familyMember.Religion, familyMemberWithDetails.Religion);
        }

        [Fact]
        public void FamilyMemberWithDetailsDtoMapToFamilyMemberWithDetails_NullInput_ReturnsNull()
        {
            //Arrange
            FamilyMemberWithDetailsDto familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }

        [Fact]
        public void FamilyMemberWithDetailsCreationDtoMapToFamilyMemberWithDetails_MapsCorrectly_ReturnsMappedAccount()
        {
            //Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetailsCreationDto
            {
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Equal(familyMember.LastName, familyMemberWithDetails.LastName);
            Assert.Equal(familyMember.FirstName, familyMemberWithDetails.FirstName);
            Assert.Equal(familyMember.Age, familyMemberWithDetails.Age);
            Assert.Equal(familyMember.MaritalStatus, familyMemberWithDetails.MaritalStatus);
            Assert.Equal(familyMember.BirthDate, familyMemberWithDetails.BirthDate);
            Assert.Equal(familyMember.Gender, familyMemberWithDetails.Gender);
            Assert.Equal(familyMember.Occupation, familyMemberWithDetails.Occupation);
            Assert.Equal(familyMember.ContactNo, familyMemberWithDetails.ContactNo);
            Assert.Equal(familyMember.Religion, familyMemberWithDetails.Religion);
        }

        [Fact]
        public void FamilyMemberWithDetailsCreationDtoMapToFamilyMemberWithDetails_NullInput_ReturnsNull()
        {
            //Arrange
            FamilyMemberWithDetailsCreationDto familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }

        [Fact]
        public void FamilyMemberWithDetailsMapToFamilyMemberWithDetailsUpdationDto_MapsCorrectly_ReturnsMappedAccount()
        {
            //Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetails
            {
                Id = 1,
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic",
                FId = 1
            };

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetailsUpdationDto>(familyMemberWithDetails);

            //Assert
            Assert.Equal(familyMember.LastName, familyMemberWithDetails.LastName);
            Assert.Equal(familyMember.FirstName, familyMemberWithDetails.FirstName);
            Assert.Equal(familyMember.Age, familyMemberWithDetails.Age);
            Assert.Equal(familyMember.MaritalStatus, familyMemberWithDetails.MaritalStatus);
            Assert.Equal(familyMember.BirthDate, familyMemberWithDetails.BirthDate);
            Assert.Equal(familyMember.Gender, familyMemberWithDetails.Gender);
            Assert.Equal(familyMember.Occupation, familyMemberWithDetails.Occupation);
            Assert.Equal(familyMember.ContactNo, familyMemberWithDetails.ContactNo);
            Assert.Equal(familyMember.Religion, familyMemberWithDetails.Religion);
        }

        [Fact]
        public void FamilyMemberWithDetailsMapToFamilyMemberWithDetailsUpdationDto_NullInput_ReturnsNull()
        {
            //Arrange
            FamilyMemberWithDetails familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetailsUpdationDto>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }

        [Fact]
        public void FamilyMemberWithDetailsUpdationDtoMapToFamilyMemberWithDetails_MapsCorrectly_ReturnsMappedAccount()
        {
            //Arrange
            var familyMemberWithDetails = new FamilyMemberWithDetailsUpdationDto
            {
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic",
            };

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Equal(familyMember.LastName, familyMemberWithDetails.LastName);
            Assert.Equal(familyMember.FirstName, familyMemberWithDetails.FirstName);
            Assert.Equal(familyMember.Age, familyMemberWithDetails.Age);
            Assert.Equal(familyMember.MaritalStatus, familyMemberWithDetails.MaritalStatus);
            Assert.Equal(familyMember.BirthDate, familyMemberWithDetails.BirthDate);
            Assert.Equal(familyMember.Gender, familyMemberWithDetails.Gender);
            Assert.Equal(familyMember.Occupation, familyMemberWithDetails.Occupation);
            Assert.Equal(familyMember.ContactNo, familyMemberWithDetails.ContactNo);
            Assert.Equal(familyMember.Religion, familyMemberWithDetails.Religion);
        }

        [Fact]
        public void FamilyMemberWithDetailsUpdationDtoMapToFamilyMemberWithDetails_NullInput_ReturnsNull()
        {
            //Arrange
            FamilyMemberWithDetailsUpdationDto familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<FamilyMemberWithDetails>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }
    }
}
