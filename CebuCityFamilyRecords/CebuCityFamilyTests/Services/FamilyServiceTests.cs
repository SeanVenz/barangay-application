using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.FamilyRepository;
using CebuCityFamilyAPI.Services.FamilyService;
using Moq;

namespace CebuCityFamilyAPITests.Services
{
    public class FamilyServiceTests
    {
        private readonly Mock<IFamilyRepository> _mockFamilyRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly FamilyService _service;

        public FamilyServiceTests()
        {
            _mockFamilyRepository = new Mock<IFamilyRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new FamilyService(_mockFamilyRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task RegisterFamilyToBarangay_ValidFamily_RegistersFamily()
        {
            //Arrange
            int id = 1;
            int bId = 1;
            var familyCreationDto = new FamilyCreationDto
            {
                Sitio = "test",
                Name = "test Fam",
            };

            var family = new Family
            {
                Id = 1,
                Barangay = new Barangay { Id = bId },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            _mockMapper.Setup(m => m.Map<Family>(It.IsAny<FamilyCreationDto>()))
                .Returns(family);
            _mockFamilyRepository.Setup(repo => repo.CreateFamily(It.IsAny<Family>()))
                .ReturnsAsync(id);

            //Act
            var result = await _service.Register(bId, familyCreationDto);

            //Assert
            Assert.Equal(id, result);
        }

        [Fact]
        public async Task RegisterFamilyToBarangay_InvalidFamily_ThrowsException()
        {
            // Arrange
            int id = 1;
            int bId = 1;
            var familyCreationDto = new FamilyCreationDto
            {
                Sitio = "test",
                Name = "test Fam",
            };

            var family = new Family
            {
                Id = 1,
                Barangay = new Barangay { Id = bId },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            _mockMapper.Setup(m => m.Map<Family>(It.IsAny<FamilyCreationDto>()))
                .Returns(family);
            _mockFamilyRepository.Setup(repo => repo.CreateFamily(It.IsAny<Family>()))
                .Throws(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.Register(bId, familyCreationDto));
        }


        [Fact]
        public async Task CreateFamily_ValidFamily_CreatesFamily()
        {
            // Arrange
            int id = 1;
            var familyDto = new FamilyCreationDto
            {
                Sitio = "test",
                Name = "test Fam",
            };

            var family = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyDtoReturn = new FamilyDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyDto))
                .Returns(family);
            _mockFamilyRepository.Setup(repo => repo.CreateFamily(family))
                .ReturnsAsync(id);
            _mockMapper.Setup(mapper => mapper.Map<FamilyDto>(It.Is<Family>(x => x.Id == 1)))
                .Returns(familyDtoReturn);

            // Act
            var result = await _service.CreateFamily(familyDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(familyDtoReturn, result);
        }

        [Fact]
        public async Task CreateFamily_RepositoryReturnsErrorCode_ReturnsNull()
        {
            // Arrange
            var familyDto = new FamilyCreationDto
            {
                Sitio = "test",
                Name = "test Fam",
            };

            var family = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyDtoReturn = new FamilyDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyDto))
                .Returns(family);
            _mockFamilyRepository.Setup(repo => repo.CreateFamily(family)).ReturnsAsync(-2); // Simulate error code 2.

            // Act
            var result = await _service.CreateFamily(familyDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateFamily_RepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            var familyCreationDto = new FamilyCreationDto
            {
                Sitio = "test",
                Name = "test Fam",
            };

            var family = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyDtoReturn = new FamilyDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam"
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyCreationDto))
                .Returns(family);
            _mockFamilyRepository.Setup(repo => repo.CreateFamily(family))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateFamily(familyCreationDto));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetAllFamilies_WhenCalled_ReturnsAllFamily()
        {
            // Arrange
            var familyModels = new List<Family>
            {
                new Family { Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
                },
                new Family { Id = 2,
                Barangay = new Barangay { },
                BId = 2,
                Sitio = "test2",
                Name = "test Fam2"
                }
            };
            var familyDtos = new List<FamilyDto>
            {
                new FamilyDto { Id = 1,
                Sitio = "test",
                Name = "test Fam"
                },
                new FamilyDto {Id = 2,
                Sitio = "test2",
                Name = "test Fam2"
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetAllFamilies())
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetAllFamilies();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllFamilies_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var familyModels = new List<Family>();
            var familyDtos = new List<FamilyDto>();

            _mockFamilyRepository.Setup(repo => repo.GetAllFamilies())
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetAllFamilies();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllFamilies_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            _mockFamilyRepository.Setup(repo => repo.GetAllFamilies())
                .ThrowsAsync(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAllFamilies());

            // Assert
            Assert.Equal("Database connection error", exception.Message);

        }

        [Fact]
        public async Task GetFamilyById_ExistingId_ReturnsFamilyDto()
        {
            // Arrange
            int id = 1;
            var familyModel = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam"
            };
            var familyDto = new FamilyDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam"
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyById(id))
                .ReturnsAsync(familyModel);
            _mockMapper.Setup(mapper => mapper.Map<FamilyDto>(familyModel))
                .Returns(familyDto);

            // Act
            var result = await _service.GetFamilyById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FamilyDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetFamilyById_NonExistingId_ReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockFamilyRepository.Setup(repo => repo.GetFamilyById(id))
                .ReturnsAsync((Family?)null);

            // Act
            var result = await _service.GetFamilyById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyById_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            _mockFamilyRepository.Setup(repo => repo.GetFamilyById(id))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyById(id));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyByBarangayId_ExistingId_ReturnsListOfFamilyDto()
        {
            // Arrange
            int brgyId = 1;
            var familyModels = new List<Family>
            {
                new Family
                {
                    Id = 1,
                    Barangay = new Barangay { },
                    BId = brgyId,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new Family
                {
                    Id = 2,
                    Barangay = new Barangay { },
                    BId = brgyId,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            var familyDtos = new List<FamilyDto>
            {
                new FamilyDto
                {
                    Id = 1,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new FamilyDto
                {
                    Id = 2,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayId(brgyId))
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetFamilyByBarangayId(brgyId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetFamilyByBarangayId_NonExistingId_ReturnsNull()
        {
            // Arrange
            int brgyId = 1;
            var familyModels = new List<Family>
            {
                new Family
                {
                    Id = 1,
                    Barangay = new Barangay { },
                    BId = 3,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new Family
                {
                    Id = 2,
                    Barangay = new Barangay { },
                    BId = 3,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            var familyDtos = new List<FamilyDto>
            {
                new FamilyDto
                {
                    Id = 1,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new FamilyDto
                {
                    Id = 2,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayId(brgyId))
                .ReturnsAsync((List<Family>?)null);

            // Act
            var result = await _service.GetFamilyByBarangayId(brgyId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyByBarangayId_Exception_ReturnsError()
        {
            // Arrange
            int brgyId = 1;
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayId(brgyId))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyByBarangayId(brgyId));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_ExistingName_ReturnsListOfFamilyDto()
        {
            // Arrange
            string brgyName = "test Fam";
            var familyModels = new List<Family>
            {
                new Family
                {
                    Id = 1,
                    Barangay = new Barangay { },
                    BId = 3,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new Family
                {
                    Id = 2,
                    Barangay = new Barangay { },
                    BId = 3,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            var familyDtos = new List<FamilyDto>
            {
                new FamilyDto
                {
                    Id = 1,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new FamilyDto
                {
                    Id = 2,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayName(brgyName))
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetFamilyByBarangayName(brgyName);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetFamilyByBarangayName_NonExistingName_ReturnsNull()
        {
            // Arrange
            string brgyName = "test";
            var familyModels = new List<Family>
            {
                new Family
                {
                    Id = 1,
                    Barangay = new Barangay { Name = brgyName},
                    BId = 3,
                    Sitio = "test",
                    Name = "test"
                },
                new Family
                {
                    Id = 2,
                    Barangay = new Barangay { Name =brgyName },
                    BId = 3,
                    Sitio = "test2",
                    Name = "test"
                }
            };
            var familyDtos = new List<FamilyDto>
            {
                new FamilyDto
                {
                    Id = 1,
                    Sitio = "test",
                    Name = "test Fam"
                },
                new FamilyDto
                {
                    Id = 2,
                    Sitio = "test2",
                    Name = "test Fam2"
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayName(brgyName))
                .ReturnsAsync((List<Family>?)null);

            // Act
            var result = await _service.GetFamilyByBarangayName(brgyName);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_Exception_ReturnsError()
        {
            // Arrange
            string name = "test";
            _mockFamilyRepository.Setup(repo => repo.GetFamilyByBarangayName(name))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyByBarangayName(name));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task UpdateFamily_IsFound_ReturnsFamilyDto()
        {
            //Arrange
            int famId = 1;
            int id = 1;
            var familyModel = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyToUpdateModel = new Family
            {
                Id = famId,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "updatetest",
                Name = "test Fam"
            };
            var familyUpdationModel = new FamilyUpdationDto
            {
                Sitio = "updatetest"
            };

            var updatedFamDto = new FamilyDto
            {
                Id = famId,
                Sitio = "updatetest",
                Name = "test Fam"
            };

            var updatedFamModel = new Family
            {
                Id = famId,
                Barangay = new Barangay(),
                Name = "updatetest",
                BId = 3,
                Sitio = "test Fam",
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyUpdationModel))
                .Returns(familyToUpdateModel);
            _mockFamilyRepository.Setup(repo => repo.UpdateFamily(familyToUpdateModel))
                .ReturnsAsync(updatedFamModel);
            _mockMapper.Setup(mapper => mapper.Map<FamilyDto>(updatedFamModel))
                .Returns(updatedFamDto);

            //Act
            var result = await _service.UpdateFamily(id, familyUpdationModel);

            //Assert
            Assert.IsType<FamilyDto>(result);
            Assert.Equal(familyUpdationModel.Sitio, result.Sitio);
        }

        [Fact]
        public async Task UpdateFamily_IsNotFound_ReturnsNull()
        {
            //Arrange
            int famId = 1;
            int id = 1;
            var familyModel = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyToUpdateModel = new Family
            {
                Id = famId,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "updatetest",
                Name = "test Fam"
            };
            var familyUpdationModel = new FamilyUpdationDto
            {
                Sitio = "updatetest"
            };

            var updatedFamDto = new FamilyDto
            {
                Id = famId,
                Sitio = "updatetest",
                Name = "test Fam"
            };

            var updatedFamModel = new Family
            {
                Id = famId,
                Barangay = new Barangay(),
                Name = "updatetest",
                BId = 3,
                Sitio = "test Fam",
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyUpdationModel))
                .Returns(familyToUpdateModel);
            _mockFamilyRepository.Setup(repo => repo.UpdateFamily(familyToUpdateModel))
                .ReturnsAsync((Family?)null);
            _mockMapper.Setup(mapper => mapper.Map<FamilyDto>(updatedFamModel))
                .Returns(updatedFamDto);

            //Act
            var result = await _service.UpdateFamily(id, familyUpdationModel);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateFamily_Exception_ReturnsError()
        {
            //Arrange
            int famId = 1;
            int id = 1;
            var familyModel = new Family
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "test",
                Name = "test Fam"
            };

            var familyToUpdateModel = new Family
            {
                Id = famId,
                Barangay = new Barangay { },
                BId = 3,
                Sitio = "updatetest",
                Name = "test Fam"
            };
            var familyUpdationModel = new FamilyUpdationDto
            {
                Sitio = "updatetest"
            };

            var updatedFamDto = new FamilyDto
            {
                Id = famId,
                Sitio = "updatetest",
                Name = "test Fam"
            };

            var updatedFamModel = new Family
            {
                Id = famId,
                Barangay = new Barangay(),
                Name = "updatetest",
                BId = 3,
                Sitio = "test Fam",
            };

            _mockMapper.Setup(mapper => mapper.Map<Family>(familyUpdationModel))
                .Throws(new Exception("Database connection error"));
            _mockFamilyRepository.Setup(repo => repo.UpdateFamily(familyToUpdateModel))
                .Throws(new Exception("Database connection error"));
            _mockMapper.Setup(mapper => mapper.Map<FamilyDto>(updatedFamModel))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.UpdateFamily(id, familyUpdationModel));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembers_WhenCalled_ReturnsAllFamilyWithMembersWithDetails()
        {
            // Arrange
            var familyModels = new List<FamilyWithMember>
            {
                new FamilyWithMember { Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetails>(){ new FamilyMemberWithDetails()}
                },
                new FamilyWithMember { Id = 2,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetails>(){ new FamilyMemberWithDetails()}
                }
            };
            var familyDtos = new List<FamilyWithMemberDto>
            {
                new FamilyWithMemberDto {  Id = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetailsDto>(){ new FamilyMemberWithDetailsDto()}
                },
                new FamilyWithMemberDto {Id = 2,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetailsDto>(){ new FamilyMemberWithDetailsDto()}
                }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembers())
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyWithMemberDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetFamilyWithFamilyMembers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyWithMemberDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembers_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var familyModels = new List<FamilyWithMember>();
            var familyDtos = new List<FamilyWithMemberDto>();
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembers())
                .ReturnsAsync(familyModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<FamilyWithMemberDto>>(familyModels))
                .Returns(familyDtos);

            // Act
            var result = await _service.GetFamilyWithFamilyMembers();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<FamilyWithMemberDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembers_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembers())
                .ThrowsAsync(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyWithFamilyMembers());

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersById_ExistingId_ReturnsFamilyWithMemberDto()
        {
            // Arrange
            int id = 1;
            var familyModel = new FamilyWithMember
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetails>() { new FamilyMemberWithDetails() }
            };
            var familyDto = new FamilyWithMemberDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetailsDto>() { new FamilyMemberWithDetailsDto() }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersById(id))
                .ReturnsAsync(familyModel);
            _mockMapper.Setup(mapper => mapper.Map<FamilyWithMemberDto>(familyModel))
                .Returns(familyDto);

            // Act
            var result = await _service.GetFamilyWithFamilyMembersById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FamilyWithMemberDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersById_NonExistingId_ReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersById(id))
                .ReturnsAsync((FamilyWithMember?)null);

            // Act
            var result = await _service.GetFamilyWithFamilyMembersById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersById_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersById(id))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyWithFamilyMembersById(id));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersByNamed_ExistingName_ReturnsFamilyWithMemberDto()
        {
            // Arrange
            string name = "test Fam2";
            var familyModel = new FamilyWithMember
            {
                Id = 1,
                Barangay = new Barangay { },
                BId = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetails>() { new FamilyMemberWithDetails() }
            };
            var familyDto = new FamilyWithMemberDto
            {
                Id = 1,
                Sitio = "test",
                Name = "test Fam2",
                FamilyMembers = new List<FamilyMemberWithDetailsDto>() { new FamilyMemberWithDetailsDto() }
            };
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersByName(name))
                .ReturnsAsync(familyModel);
            _mockMapper.Setup(mapper => mapper.Map<FamilyWithMemberDto>(familyModel))
                .Returns(familyDto);

            // Act
            var result = await _service.GetFamilyWithFamilyMembersByName(name);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<FamilyWithMemberDto>(result);
            Assert.Equal(name, result.Name);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersByName_NonExistingId_ReturnsNull()
        {
            // Arrange
            string name = "test Fam2";
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersByName(name))
                .ReturnsAsync((FamilyWithMember?)null);

            // Act
            var result = await _service.GetFamilyWithFamilyMembersByName(name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetFamilyWithFamilyMembersByName_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            string name = "test Fam2";
            _mockFamilyRepository.Setup(repo => repo.GetFamilyWithFamilyMembersByName(name))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetFamilyWithFamilyMembersByName(name));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }
    }
}
