using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.FamilyMembersRepository;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using Moq;

namespace CebuCityFamilyAPITests.Services
{
    public class FamilyMemberServiceTests
    {
        private readonly Mock<IFamilyMembersRepository> _mockFamilyMemberRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly FamilyMembersService _service;

        public FamilyMemberServiceTests()
        {
            _mockFamilyMemberRepository = new Mock<IFamilyMembersRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new FamilyMembersService(_mockFamilyMemberRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_WhenCalled_ReturnsAllFamilyMemberWithDetails()
        {
            //Arrange
            var familyMemberModel = new List<FamilyMemberWithDetails>
            {
                new FamilyMemberWithDetails { Id = 1, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123456789",Religion = "Test",FId = 1},
                new FamilyMemberWithDetails { Id = 2, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123423789",Religion = "Test",FId = 1}
            };
            var familyMemberDto = new List<FamilyMemberWithDetailsDto>
            {
                new FamilyMemberWithDetailsDto {Id = 1, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123456789",Religion = "Test"},
                new FamilyMemberWithDetailsDto { Id = 2, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123423789",Religion = "Test"}
            };

            _mockFamilyMemberRepository.Setup(repo => repo.GetAllFamilyMemberWithDetails())
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(familyMemberModel))
                .Returns(familyMemberDto);

            //Act
            var result = await _service.GetAllFamilyMemberWithDetails();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyMemberWithDetailsDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_WhenCalled_ReturnsEmpty()
        {
            //Arrange
            var familyMemberModel = new List<FamilyMemberWithDetails>();
            var familyMemberDto = new List<FamilyMemberWithDetailsDto>();


            _mockFamilyMemberRepository.Setup(repo => repo.GetAllFamilyMemberWithDetails())
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(familyMemberModel))
                .Returns(familyMemberDto);

            //Act
            var result = await _service.GetAllFamilyMemberWithDetails();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyMemberWithDetailsDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_WhenCalled_ThrowsException()
        {
            //Arrange
            _mockFamilyMemberRepository.Setup(repo => repo.GetAllFamilyMemberWithDetails())
                .ThrowsAsync(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAllFamilyMemberWithDetails());

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsById_HasFamilyMember_ReturnsFamilyMember()
        {
            //Arrange
            int id = 1;
            var familyMemberModel = new FamilyMemberWithDetails
            {
                Id = id,
                LastName = "Test",
                FirstName = "Test",
                Age = 1,
                MaritalStatus = "Test",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "Test",
                ContactNo = "+639123456789",
                Religion = "Test",
                FId = 1
            };

            var familyMemberDto = new FamilyMemberWithDetailsDto
            {
                Id = id,
                LastName = "Test",
                FirstName = "Test",
                Age = 1,
                MaritalStatus = "Test",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "Test",
                ContactNo = "+639123456789",
                Religion = "Test"
            };

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetailsDto>(familyMemberModel))
                .Returns(familyMemberDto);

            //Act
            var result = await _service.GetFamilyMemberWithDetailsById(id);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<FamilyMemberWithDetailsDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsById_NonExistingId_ReturnsNull()
        {
            //Arrange
            int id = 1;

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetails?)null);

            //Act
            var result = await _service.GetFamilyMemberWithDetailsById(id);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsById_WhenRepositoryFails_ThrowsException()
        {
            //Arrange
            int id = 1;

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ThrowsAsync(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyMemberWithDetailsById(id));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsByName_HasFamilyMember_ReturnsFamilyMember()
        {
            //Arrange
            string lastName = "Test";
            var familyMemberModel = new List<FamilyMemberWithDetails>
            {
                new FamilyMemberWithDetails { Id = 1, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123456789",Religion = "Test",FId = 1},
                new FamilyMemberWithDetails { Id = 2, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123423789",Religion = "Test",FId = 1}
            };
            var familyMemberDto = new List<FamilyMemberWithDetailsDto>
            {
                new FamilyMemberWithDetailsDto {Id = 1, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123456789",Religion = "Test"},
                new FamilyMemberWithDetailsDto { Id = 2, LastName = "Test", FirstName = "Test",Age = 1,MaritalStatus = "Test",BirthDate = "12-23-2001",Gender = "Male",Occupation = "Test",ContactNo = "+639123423789",Religion = "Test"}
            };

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsByName(lastName))
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(familyMemberModel))
                .Returns(familyMemberDto);

            //Act
            var result = await _service.GetFamilyMemberWithDetailsByName(lastName);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyMemberWithDetailsDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsByName_HasNoFamilyMember_ReturnsEmpty()
        {
            //Arrange
            string lastName = "Test";
            var familyMemberModel = new List<FamilyMemberWithDetails>();
            var familyMemberDto = new List<FamilyMemberWithDetailsDto>();

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsByName(lastName))
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(familyMemberModel))
                .Returns(familyMemberDto);

            //Act
            var result = await _service.GetFamilyMemberWithDetailsByName(lastName);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyMemberWithDetailsDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsByName_WhenFamilyMemberDoesNotExist_ReturnsNull()
        {
            // Arrange
            string lastName = "NonExistingLastName";
            List<FamilyMemberWithDetails>? familyMemberModel = null;
            List<FamilyMemberWithDetailsDto>? familyMemberDto = null;

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsByName(lastName))
                .ReturnsAsync(familyMemberModel);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(It.IsAny<IEnumerable<FamilyMemberWithDetails>>()))
                .Returns(familyMemberDto);

            // Act
            var result = await _service.GetFamilyMemberWithDetailsByName(lastName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyMemberWithDetailsByName_WhenRepositoryFails_ThrowsException()
        {
            //Arrange
            string lastName = "Test";

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsByName(lastName))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyMemberWithDetailsByName(lastName));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_ValidDetails_ReturnsFamilyMember()
        {
            // Arrange
            var id = 1;
            var familyMemberCreationDto = new FamilyMemberWithDetailsCreationDto
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
            var familyMemberModel = new FamilyMemberWithDetails
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
                FId = id
            };
            var familyMemberDto = new FamilyMemberWithDetailsDto
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

            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberCreationDto))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.CreateFamilyMemberWithDetails(id, familyMemberModel))
                .ReturnsAsync(id);
            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetailsDto>(familyMemberModel))
                .Returns(familyMemberDto);

            // Act
            var result = await _service.CreateFamilyMemberWithDetails(id, familyMemberCreationDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FamilyMemberWithDetailsDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_RepositoryReturnsErrorCode_ReturnsNull()
        {
            // Arrange
            var id = 1;
            var familyMemberCreationDto = new FamilyMemberWithDetailsCreationDto
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
            var familyMemberModel = new FamilyMemberWithDetails
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
                FId = id
            };
            var familyMemberDto = new FamilyMemberWithDetailsDto
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

            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberCreationDto))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.CreateFamilyMemberWithDetails(id, familyMemberModel))
                .ReturnsAsync(-2);

            // Act
            var result = await _service.CreateFamilyMemberWithDetails(id, familyMemberCreationDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_RepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            var familyMemberCreationDto = new FamilyMemberWithDetailsCreationDto
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
            var familyMemberModel = new FamilyMemberWithDetails
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
                FId = id
            };

            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberCreationDto))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.CreateFamilyMemberWithDetails(id, familyMemberModel))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateFamilyMemberWithDetails(id, familyMemberCreationDto));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task UpdateFamilyMemberWithDetails_IsFound_ReturnsTrue()
        {
            //Arrange
            int famId = 1;
            int id = 1;
            var familyMemberModel = new FamilyMemberWithDetails
            {
                Id = id,
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic",
                FId = famId
            };
            var familyMemberUpdationModel = new FamilyMemberWithDetailsUpdationDto
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

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetails());
            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberUpdationModel))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.UpdateFamilyMemberWithDetails(familyMemberModel))
                .ReturnsAsync(true);

            //Act
            var result = await _service.UpdateFamilyMemberWithDetails(id, familyMemberUpdationModel);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateFamilyMemberWithDetails_IsNotFound_ReturnsTrue()
        {
            //Arrange
            int id = 1;

            var familyMemberModel = new FamilyMemberWithDetails();
            var familyMemberUpdationModel = new FamilyMemberWithDetailsUpdationDto();

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetails?)null);
            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberUpdationModel))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.UpdateFamilyMemberWithDetails(familyMemberModel))
                .ReturnsAsync(false);

            //Act
            var result = await _service.UpdateFamilyMemberWithDetails(id, familyMemberUpdationModel);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateFamilyMemberWithDetails_WhenRepositoryFails_ReturnsError()
        {
            //Arrange
            int id = 1;
            int famId = 1;
            var familyMemberModel = new FamilyMemberWithDetails
            {
                Id = id,
                LastName = "Test",
                FirstName = "Test",
                Age = 10,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Male",
                Occupation = "CPA",
                ContactNo = "+639123456789",
                Religion = "Catholic",
                FId = famId
            };
            var familyMemberUpdationModel = new FamilyMemberWithDetailsUpdationDto
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

            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .Throws(new Exception("Database connection error"));
            _mockMapper.Setup(mapper => mapper.Map<FamilyMemberWithDetails>(familyMemberUpdationModel))
                .Returns(familyMemberModel);
            _mockFamilyMemberRepository.Setup(repo => repo.UpdateFamilyMemberWithDetails(familyMemberModel))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.UpdateFamilyMemberWithDetails(id, familyMemberUpdationModel));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task DeleteFamilyMemberWithDetails_ValidId_ReturnsTrue()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetails());
            _mockFamilyMemberRepository.Setup(repo => repo.DeleteFamilyMemberWithDetails(id))
                .ReturnsAsync(true);

            //Act
            var result = await _service.DeleteFamilyMemberWithDetails(id);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteFamilyMemberWithDetails_InvalidId_ReturnsFalse()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetails?)null);
            _mockFamilyMemberRepository.Setup(repo => repo.DeleteFamilyMemberWithDetails(id))
                .ReturnsAsync(false);

            //Act
            var result = await _service.DeleteFamilyMemberWithDetails(id);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteFamilyMemberWithDetails_WhenRepositoryFailse_ReturnsError()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberRepository.Setup(repo => repo.GetFamilyMemberWithDetailsById(id))
                .Throws(new Exception("Database connection error"));
            _mockFamilyMemberRepository.Setup(repo => repo.DeleteFamilyMemberWithDetails(id))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.DeleteFamilyMemberWithDetails(id));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }
    }
}