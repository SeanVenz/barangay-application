using CebuCityFamilyAPI.Controllers;
using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Services.BarangayService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CebuCityFamilyTests.Controllers
{
    public class BarangaysControllerTests
    {
        private readonly BarangaysController _controller;
        private readonly Mock<IBarangayService> _mockBarangayService;
        private readonly Mock<IFamilyService> _mockFamilyService;
        private readonly Mock<ILogger<BarangaysController>> _mockLogger;

        public BarangaysControllerTests()
        {
            _mockBarangayService = new Mock<IBarangayService>();
            _mockFamilyService = new Mock<IFamilyService>();
            _mockLogger = new Mock<ILogger<BarangaysController>>();

            _controller = new BarangaysController(_mockBarangayService.Object, _mockFamilyService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllBarangays_HasUBarangays_ReturnsOk()
        {
            // Arrange
            _mockBarangayService.Setup(service => service.GetAllBarangays())
                .ReturnsAsync(new List<BarangayDto> { new BarangayDto() });

            // Act 
            var result = await _controller.GetBarangays();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllBarangays_HasNoBarangays_ReturnsNoContent()
        {
            // Arrange
            _mockBarangayService.Setup(service => service.GetAllBarangays())
                .ReturnsAsync(new List<BarangayDto>());

            // Act 
            var result = await _controller.GetBarangays();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllBarangays_Exception_ReturnsServerError()
        {
            // Arrange
            _mockBarangayService.Setup(service => service.GetAllBarangays())
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetBarangays();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetBarangaysById_BarangayExistsHasFamily_ReturnsOk()
        {
            // Arrange
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayId(id))
                .ReturnsAsync(new List<FamilyDto>() { new FamilyDto() });

            // Act 
            var result = await _controller.GetBarangayById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangaysById_BarangayExistsHasNoFamily_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayId(id))
                .ReturnsAsync(new List<FamilyDto>());

            // Act 
            var result = await _controller.GetBarangayById(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetBarangaysById_BarangayDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayId(id))
                .ReturnsAsync(new List<FamilyDto>() { new FamilyDto() });

            // Act 
            var result = await _controller.GetBarangayById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangaysById_Exception_ReturnsServerError()
        {
            // Arrange
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .Throws(new Exception());
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayId(id))
                .ReturnsAsync(new List<FamilyDto>() { new FamilyDto() });

            // Act 
            var result = await _controller.GetBarangayById(id);


            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetBarangayPopulationById_BarangayExists_ReturnsOk()
        {
            // Arrange
            var brgy = new Barangay()
            {
                Families = new List<Family>()
                {
                    new Family { Id = 1, },
                    new Family { Id = 2, }
                }
            };
            int id = 1;
            int population = brgy.Families.Count;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());
            _mockBarangayService.Setup(service => service.GetPopulationInBarangay(id))
                .ReturnsAsync(brgy.Families.Count);

            var expectedPopulationResult = new ObjectResult(population);

            // Act 
            var result = await _controller.GetBarangayPopulationById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(expectedPopulationResult.Value, okResult.Value);
        }

        [Fact]
        public async Task GetBarangayPopulationById_BarangayDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var brgy = new Barangay()
            {
                Families = new List<Family>()
                {
                    new Family { Id = 1, },
                    new Family { Id = 2, }
                }
            };
            int id = 1;
            int population = brgy.Families.Count;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));
            _mockBarangayService.Setup(service => service.GetPopulationInBarangay(id))
                .ReturnsAsync(brgy.Families.Count);

            var expectedPopulationResult = new ObjectResult(population);

            // Act 
            var result = await _controller.GetBarangayPopulationById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangayPopulationById_Exception_ReturnsServerError()
        {
            // Arrange
            var brgy = new Barangay()
            {
                Families = new List<Family>()
                {
                    new Family { Id = 1, },
                    new Family { Id = 2, }
                }
            };
            int id = 1;
            int population = brgy.Families.Count;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());
            _mockBarangayService.Setup(service => service.GetPopulationInBarangay(id))
                .Throws(new Exception());

            var expectedPopulationResult = new ObjectResult(population);

            // Act 
            var result = await _controller.GetBarangayPopulationById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetBarangaysByName_BarangayExists_ReturnsOk()
        {
            // Arrange
            string name = "test";
            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(new BarangayDto());

            // Act 
            var result = await _controller.GetBarangayByName(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangaysByName_BarangayDoesnotExists_ReturnsNotFound()
        {
            // Arrange
            string name = "test";
            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync((BarangayDto?)null);

            // Act 
            var result = await _controller.GetBarangayByName(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangaysByName_BarangayDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            string name = "test";
            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));

            // Act 
            var result = await _controller.GetBarangayByName(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetBarangaysByName_Exception_ReturnsServerError()
        {
            // Arrange
            string name = "test";
            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .Throws(new Exception());
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayName(name))
                .ReturnsAsync(new List<FamilyDto>());

            // Act 
            var result = await _controller.GetBarangayByName(name);


            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateBarangay_ValidBarangay_ReturnsOk()
        {
            // Arrange
            var brgy = new BarangayCreationDto()
            {
                Name = "testman",
                Captain = "Cpt. test"
            };

            _mockBarangayService.Setup(service => service.CreateBarangay(brgy))
                .ReturnsAsync(new BarangayDto());

            // Act 
            var result = await _controller.CreateBarangay(brgy);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateBarangay_BarangayExists_ReturnsConflict()
        {
            // Arrange
            var brgy = new BarangayCreationDto()
            {
                Name = "testman",
                Captain = "Cpt. test"
            };

            _mockBarangayService.Setup(service => service.CreateBarangay(brgy))
                .ReturnsAsync(new BarangayDto());
            int exists = 1;
            _mockBarangayService.Setup(service => service.CountBarangayName(brgy.Name))
                .ReturnsAsync(exists);
            // Act 
            var result = await _controller.CreateBarangay(brgy);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(409, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateBarangay_Exception_ReturnsServerError()
        {
            // Arrange
            var brgy = new BarangayCreationDto()
            {
                Name = "testman",
                Captain = "Cpt. test"
            };

            _mockBarangayService.Setup(service => service.CreateBarangay(brgy))
                .Throws(new Exception());

            // Act 
            var result = await _controller.CreateBarangay(brgy);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task RegisterFamily_ValidBarangayAndValidFamily_ReturnsOk()
        {
            // Arrange
            var family = new FamilyCreationDto
            {
                Sitio = "testa",
                Name = "kobe"
            };
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());
            _mockFamilyService.Setup(service => service.Register(id, family))
                .ReturnsAsync(id);

            // Act 
            var result = await _controller.RegisterFamily(id, family);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task RegisterFamily_InvalidBarangay_ReturnsNotFound()
        {
            // Arrange
            var family = new FamilyCreationDto
            {
                Sitio = "testa",
                Name = "kobe"
            };
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));
            _mockFamilyService.Setup(service => service.Register(id, family))
                .ReturnsAsync(id);

            // Act 
            var result = await _controller.RegisterFamily(id, family);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task RegisterFamily_Exception_ReturnsServerError()
        {
            // Arrange
            var Family = new FamilyCreationDto
            {
                Sitio = "testa",
                Name = "test"
            };
            int id = 1;
            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.RegisterFamily(id, Family);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateBarangay_BarangayExists_ReturnsOk()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            int id = 1;

            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(new BarangayDto());

            // Act 
            var result = await _controller.UpdateBarangay(id, updateBrgy);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBarangay_BarangayDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            int id = 1;

            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));

            // Act 
            var result = await _controller.UpdateBarangay(id, updateBrgy);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBarangay_Exception_ReturnsServerError()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            int id = 1;

            _mockBarangayService.Setup(service => service.GetBarangayById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.UpdateBarangay(id, updateBrgy);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateBarangayByName_BarangayExists_ReturnsOk()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(new BarangayDto());

            // Act 
            var result = await _controller.UpdateBarangayByName(name, updateBrgy);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBarangayByName_BarangayDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));

            // Act 
            var result = await _controller.UpdateBarangayByName(name, updateBrgy);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateBarangayByName_Exception_ReturnsServerError()
        {
            // Arrange
            var updateBrgy = new BarangayUpdationDto()
            {
                Captain = "new cap",
                Name = "new name"
            };

            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .Throws(new Exception());

            // Act 
            var result = await _controller.UpdateBarangayByName(name, updateBrgy);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteBarangay_ValidBarangay_ReturnsOk()
        {
            // Arrange
            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(new BarangayDto());

            // Act 
            var result = await _controller.DeleteBarangay(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBarangay_InvalidBarangay_ReturnsNotFound()
        {
            // Arrange
            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .ReturnsAsync(await Task.FromResult<BarangayDto>(null));

            // Act 
            var result = await _controller.DeleteBarangay(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteBarangay_Exception_ReturnsServerError()
        {
            // Arrange
            string name = "test";

            _mockBarangayService.Setup(service => service.GetBarangayByName(name))
                .Throws(new Exception());

            // Act 
            var result = await _controller.DeleteBarangay(name);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
