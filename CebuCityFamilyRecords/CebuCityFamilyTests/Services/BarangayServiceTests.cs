using AutoMapper;
using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.BarangayRepository;
using CebuCityFamilyAPI.Services.BarangayService;
using Moq;

namespace CebuCityFamilyAPITests.Services
{
    public class BarangayServiceTests
    {
        private readonly Mock<IBarangayRepository> _mockBarangayRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BarangayService _service;

        public BarangayServiceTests()
        {
            _mockBarangayRepository = new Mock<IBarangayRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new BarangayService(_mockBarangayRepository.Object, _mockMapper.Object);
        }
        [Fact]
        public async Task CreateBarangay_ValidBarangay_CreatesBarangay()
        {
            // Arrange
            int id = 1;
            var barangayDto = new BarangayCreationDto
            {
                Name = "Foo",
                Captain = "Manong Test"
            };

            var barangay = new Barangay
            {
                Id = 1,
                Name = "Foo",
                Captain = "Manong Test",
                Families = { new Family() }
            };

            var BarangayDtoReturn = new BarangayDto
            {
                Id = 1,
                Name = "Foo",
                Captain = "Manong Test"
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayDto))
                .Returns(barangay);
            _mockBarangayRepository.Setup(repo => repo.CreateBarangay(barangay))
                .ReturnsAsync(id);
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(It.Is<Barangay>(x => x.Id == 1)))
                .Returns(BarangayDtoReturn);

            // Act
            var result = await _service.CreateBarangay(barangayDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(BarangayDtoReturn, result);
        }

        [Fact]
        public async Task CreateBarangay_RepositoryFails_ReturnsError()
        {
            // Arrange
            var barangayCreationDto = new BarangayCreationDto
            {
                Name = "Foo",
                Captain = "Manong Test"
            };

            var barangay = new Barangay
            {
                Id = 1,
                Name = "Foo",
                Captain = "Manong Test",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayCreationDto))
                .Returns(barangay);
            _mockBarangayRepository.Setup(repo => repo.CreateBarangay(barangay))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateBarangay(barangayCreationDto));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task CreateBarangay_RepositoryReturnsErrorCode_ReturnsNull()
        {
            // Arrange
            var barangayDto = new BarangayCreationDto
            {
                Name = "Foo",
                Captain = "Manong Test"
            };

            var barangay = new Barangay
            {
                Id = 1,
                Name = "Foo",
                Captain = "Manong Test",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayDto)).Returns(barangay);
            _mockBarangayRepository.Setup(repo => repo.CreateBarangay(barangay)).ReturnsAsync(-2); // Simulate error code 2.

            // Act
            var result = await _service.CreateBarangay(barangayDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllBarangays_WhenCalled_ReturnsAllBarangay()
        {
            // Arrange
            var barangayModels = new List<Barangay>
            {
                new Barangay { Id = 1,
                Name = "Foo",
                Captain = "Manong Test",
                Families = { new Family() }
                },
                new Barangay { Id = 2,
                Name = "Bar",
                Captain = "Manang Test",
                Families = { new Family() }
                }
            };
            var barangayDtos = new List<BarangayDto>
            {
                new BarangayDto { Id = 1,
                Name = "Foo",
                Captain = "Manong Test"
                },
                new BarangayDto {Id = 2,
                Name = "Bar",
                Captain = "Manang Test"
                }
            };
            _mockBarangayRepository.Setup(repo => repo.GetAllBarangays())
                .ReturnsAsync(barangayModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<BarangayDto>>(barangayModels))
                .Returns(barangayDtos);

            // Act
            var result = await _service.GetAllBarangays();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BarangayDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllBarangays_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var barangayModels = new List<Barangay>();
            var barangayDtos = new List<BarangayDto>();

            _mockBarangayRepository.Setup(repo => repo.GetAllBarangays())
                .ReturnsAsync(barangayModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<BarangayDto>>(barangayModels))
                .Returns(barangayDtos);

            // Act
            var result = await _service.GetAllBarangays();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BarangayDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllBarangays_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            _mockBarangayRepository.Setup(repo => repo.GetAllBarangays())
                .ThrowsAsync(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAllBarangays());

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetBarangayById_ExistingId_ReturnsBarangayDto()
        {
            // Arrange
            int id = 1;
            var barangayModel = new Barangay
            {
                Id = id,
                Name = "Foo",
                Captain = "Manong Test",
                Families = { new Family() }
            };
            var barangayDto = new BarangayDto
            {
                Id = id,
                Name = "Foo",
                Captain = "Manong Test"
            };
            _mockBarangayRepository.Setup(repo => repo.GetBarangayById(id))
                .ReturnsAsync(barangayModel);
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(barangayModel))
                .Returns(barangayDto);

            // Act
            var result = await _service.GetBarangayById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BarangayDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetBarangayById_NonExistingId_ReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockBarangayRepository.Setup(repo => repo.GetBarangayById(id))
                .ReturnsAsync((Barangay?)null);

            // Act
            var result = await _service.GetBarangayById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBarangayById_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            _mockBarangayRepository.Setup(repo => repo.GetBarangayById(id))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetBarangayById(id));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetBarangayByName_ExistingName_ReturnsBarangayDto()
        {
            // Arrange
            int id = 1;
            string name = "Foo";
            var barangayModel = new Barangay
            {
                Id = id,
                Name = name,
                Captain = "Manong Test",
                Families = { new Family() }
            };
            var barangayDto = new BarangayDto
            {
                Id = id,
                Name = name,
                Captain = "Manong Test"
            };
            _mockBarangayRepository.Setup(repo => repo.GetBarangayByName(name))
                .ReturnsAsync(barangayModel);
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(barangayModel))
                .Returns(barangayDto);

            // Act
            var result = await _service.GetBarangayByName(name);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BarangayDto>(result);
            Assert.Equal(name, result.Name);
        }

        [Fact]
        public async Task GetBarangayByName_NonExistingName_ReturnsNull()
        {
            // Arrange
            string name = "foo";
            _mockBarangayRepository.Setup(repo => repo.GetBarangayByName(name))
                .ReturnsAsync((Barangay?)null);

            // Act
            var result = await _service.GetBarangayByName(name);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBarangayByName_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            string name = "foo";
            _mockBarangayRepository.Setup(repo => repo.GetBarangayByName(name))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetBarangayByName(name));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task UpdateBarangay_IsFound_ReturnsBarangayDto()
        {
            //Arrange
            int brgyId = 1;
            int id = 1;
            var barangayModel = new Barangay
            {
                Id = brgyId,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            var barangayToUpdateModel = new Barangay
            {
                Id = brgyId,
                Name = "updated manong",
                Captain = "Foo update",
                Families = { new Family() }
            };
            var barangayUpdationModel = new BarangayUpdationDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBrgyDto = new BarangayDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBarangayModel = new Barangay
            {
                Id = brgyId,
                Name = "updated manong",
                Captain = "Foo update",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Returns(barangayToUpdateModel);
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangay(barangayToUpdateModel))
                .ReturnsAsync(updatedBarangayModel);
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(updatedBarangayModel))
                .Returns(updatedBrgyDto);

            //Act
            var result = await _service.UpdateBarangay(id, barangayUpdationModel);

            //Assert
            Assert.IsType<BarangayDto>(result);
            Assert.Equal(barangayUpdationModel.Captain, result.Captain);
            Assert.Equal(barangayUpdationModel.Name, result.Name);
        }

        [Fact]
        public async Task UpdateBarangay_IsNotFound_ReturnsNull()
        {
            //Arrange
            int id = 1;

            var barangayModel = new Barangay();
            var barangayUpdationModel = new BarangayUpdationDto();

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Returns(barangayModel);
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangay(barangayModel))
                .ReturnsAsync((Barangay?)null);

            //Act
            var result = await _service.UpdateBarangay(id, barangayUpdationModel);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBarangay_WhenRepositoryFails_ReturnsError()
        {
            //Arrange
            int brgyId = 1;
            int id = 1;
            var barangayModel = new Barangay
            {
                Id = brgyId,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            var barangayToUpdateModel = new Barangay
            {
                Id = brgyId,
                Name = "updated manong",
                Captain = "Foo update",
                Families = { new Family() }
            };
            var barangayUpdationModel = new BarangayUpdationDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBrgyDto = new BarangayDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBarangayModel = new Barangay
            {
                Id = brgyId,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Throws(new Exception("Database connection error"));
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangay(barangayModel))
                .Throws(new Exception("Database connection error"));
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(updatedBarangayModel))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.UpdateBarangay(id, barangayUpdationModel));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task UpdateBarangayByName_IsFound_ReturnsBarangayDto()
        {
            //Arrange
            string name = "manong";
            string barangayName = "manong";
            var barangayModel = new Barangay
            {
                Id = 1,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            var barangayToUpdateModel = new Barangay
            {
                Id = 1,
                Name = "updated manong",
                Captain = "Foo update",
                Families = { new Family() }
            };
            var barangayUpdationModel = new BarangayUpdationDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBrgyDto = new BarangayDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBarangayModel = new Barangay
            {
                Id = 1,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Returns(barangayToUpdateModel);
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangayByName(name, barangayToUpdateModel))
                .ReturnsAsync(updatedBarangayModel);
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(updatedBarangayModel))
                .Returns(updatedBrgyDto);

            //Act
            var result = await _service.UpdateBarangayByName(name, barangayUpdationModel);

            //Assert
            Assert.IsType<BarangayDto>(result);
            Assert.Equal(barangayUpdationModel.Captain, result.Captain);
            Assert.Equal(barangayUpdationModel.Name, result.Name);
        }

        [Fact]
        public async Task UpdateBarangayByName_IsNotFound_ReturnsNull()
        {
            //Arrange
            string name = "manong";

            var barangayModel = new Barangay();
            var barangayUpdationModel = new BarangayUpdationDto();

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Returns(barangayModel);
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangayByName(name, barangayModel))
                .ReturnsAsync((Barangay?)null);

            //Act
            var result = await _service.UpdateBarangayByName(name, barangayUpdationModel);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateBarangayByName_WhenRepositoryFails_ReturnsError()
        {
            //Arrange
            string name = "manong";
            string barangayName = "manong";
            var barangayModel = new Barangay
            {
                Id = 1,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            var barangayToUpdateModel = new Barangay
            {
                Id = 1,
                Name = "updated manong",
                Captain = "Foo update",
                Families = { new Family() }
            };
            var barangayUpdationModel = new BarangayUpdationDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBrgyDto = new BarangayDto
            {
                Captain = "updated manong",
                Name = "Foo update"
            };

            var updatedBarangayModel = new Barangay
            {
                Id = 1,
                Name = "manong",
                Captain = "Foo",
                Families = { new Family() }
            };

            _mockMapper.Setup(mapper => mapper.Map<Barangay>(barangayUpdationModel))
                .Throws(new Exception("Database connection error"));
            _mockBarangayRepository.Setup(repo => repo.UpdateBarangay(barangayModel))
                .Throws(new Exception("Database connection error"));
            _mockMapper.Setup(mapper => mapper.Map<BarangayDto>(updatedBarangayModel))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.UpdateBarangayByName(name, barangayUpdationModel));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task DeleteBarangay_ValidName_ReturnsRowsAffected()
        {
            // Arrange
            string name = "test";
            int rowsAffected = 1;
            _mockBarangayRepository.Setup(repo => repo.DeleteBarangay(name))
                .ReturnsAsync(rowsAffected);

            // Act
            var result = await _service.DeleteBarangay(name);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBarangay_InvalidId_ReturnsError()
        {
            // Arrange
            string name = "test";
            int rowsAffected = 0;
            _mockBarangayRepository.Setup(repo => repo.DeleteBarangay(name))
                .ReturnsAsync(rowsAffected);

            // Act
            var result = await _service.DeleteBarangay(name);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteBarangay_Exception_ReturnsReturnsRowsAffected()
        {
            // Arrange
            string name = "test";
            int rowsAffected = 1;
            _mockBarangayRepository.Setup(repo => repo.DeleteBarangay(name))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.DeleteBarangay(name));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task CountBarangayName_ValidName_ReturnsCount()
        {
            // Arrange
            string name = "test";
            int count = 1;
            _mockBarangayRepository.Setup(repo => repo.CountBarangayName(name))
                .ReturnsAsync(count);

            //Act
            var result = await _service.CountBarangayName(name);

            //Assert
            Assert.Equal(count, result);
        }

        [Fact]
        public async Task CountBarangayName_Exception_ReturnsError()
        {
            // Arrange
            string name = "test";
            int count = 1;
            _mockBarangayRepository.Setup(repo => repo.CountBarangayName(name))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CountBarangayName(name));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetPopulationInBarangay_ValidName_ReturnsPopulation()
        {
            // Arrange
            int id = 1;
            int population = 1;
            _mockBarangayRepository.Setup(repo => repo.GetPopulationInBarangay(id))
                .ReturnsAsync(population);

            //Act
            var result = await _service.GetPopulationInBarangay(id);

            //Assert
            Assert.Equal(population, result);
        }

        [Fact]
        public async Task GetPopulationInBarangay_Exception_ReturnsError()
        {
            // Arrange
            int id = 1;
            int population = 1;
            _mockBarangayRepository.Setup(repo => repo.GetPopulationInBarangay(id))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetPopulationInBarangay(id));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }
    }
}
