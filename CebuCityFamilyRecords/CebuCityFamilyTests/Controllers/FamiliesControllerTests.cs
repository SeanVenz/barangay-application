using CebuCityFamilyAPI.Controllers;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CebuCityFamilyTests.Controllers
{
    public class FamiliesControllerTests
    {
        private readonly FamiliesController _controller;
        private readonly Mock<IFamilyMembersService> _mockFamilyMembersService;
        private readonly Mock<IFamilyService> _mockFamilyService;
        private readonly Mock<ILogger<FamiliesController>> _mockLogger;

        public FamiliesControllerTests()
        {
            _mockFamilyMembersService = new Mock<IFamilyMembersService>();
            _mockFamilyService = new Mock<IFamilyService>();
            _mockLogger = new Mock<ILogger<FamiliesController>>();

            _controller = new FamiliesController(_mockFamilyService.Object, _mockFamilyMembersService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetFamilies_HasFamilies_ReturnsOk()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetAllFamilies())
                .ReturnsAsync(new List<FamilyDto> { new FamilyDto() });

            // Act 
            var result = await _controller.GetFamilies();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamilies_HasNoFamilies_ReturnsNoContent()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetAllFamilies())
                .ReturnsAsync(new List<FamilyDto>());

            // Act 
            var result = await _controller.GetFamilies();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetFamilies_Exception_ReturnsServerError()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetAllFamilies())
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetFamilies();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetFamilyById_FamilyExistsHasMembers_ReturnsOk()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersById(id))
                .ReturnsAsync(new List<FamilyMembersDto>() { new FamilyMembersDto() });

            // Act 
            var result = await _controller.GetFamilyById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamilyById_FamilyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(await Task.FromResult<FamilyDto>(null));
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersById(id))
                .ReturnsAsync(new List<FamilyMembersDto>() { new FamilyMembersDto() });

            // Act 
            var result = await _controller.GetFamilyById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetFamilyById_FamilyExistsHasNoMembers_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersById(id))
                .ReturnsAsync(new List<FamilyMembersDto>() { });

            // Act 
            var result = await _controller.GetFamilyById(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetFamilyById_Exception_ReturnsServerError()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .Throws(new Exception());
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersById(id))
                .ReturnsAsync(new List<FamilyMembersDto>() { new FamilyMembersDto() });
            // Act 
            var result = await _controller.GetFamilyById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_FamilyExistsHasMembers_ReturnsOk()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayName(name))
                .ReturnsAsync(new List<FamilyDto>() { new FamilyDto() });
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersByName(name))
                .ReturnsAsync(new List<FamilyMembersDto> { new FamilyMembersDto() });

            // Act 
            var result = await _controller.GetFamilyByBarangayName(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_FamilyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayName(name))
                .ReturnsAsync(new List<FamilyDto>());
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersByName(name))
                .ReturnsAsync(new List<FamilyMembersDto> { new FamilyMembersDto() });

            // Act 
            var result = await _controller.GetFamilyByBarangayName(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_FamilytExistHasNoMembers_ReturnsNoContent()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayName(name))
                .ReturnsAsync(new List<FamilyDto>() { new FamilyDto() });
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersByName(name))
                .ReturnsAsync(new List<FamilyMembersDto>());

            // Act 
            var result = await _controller.GetFamilyByBarangayName(name);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetFamilyByBarangayName_Exception_ReturnsServerError()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyByBarangayName(name))
                .Throws(new Exception());
            _mockFamilyMembersService.Setup(service => service.GetFamilyMembersByName(name))
                .ReturnsAsync(new List<FamilyMembersDto> { new FamilyMembersDto() });

            // Act 
            var result = await _controller.GetFamilyByBarangayName(name);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateFamily_FamilyExists_ReturnsOk()
        {
            // Arrange
            var updateFamily = new FamilyUpdationDto()
            {
                Sitio = "test"
            };

            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());

            // Act 
            var result = await _controller.UpdateFamily(id, updateFamily);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFamily_FamilyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var updateFamily = new FamilyUpdationDto()
            {
                Sitio = "test"
            };

            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(await Task.FromResult<FamilyDto>(null));

            // Act 
            var result = await _controller.UpdateFamily(id, updateFamily);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFamily_Exception_ReturnsServerError()
        {
            // Arrange
            var updateFamily = new FamilyUpdationDto()
            {
                Sitio = "test"
            };

            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.UpdateFamily(id, updateFamily);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteFamily_ValidFamily_ReturnsOk()
        {
            // Arrange
            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());

            // Act 
            var result = await _controller.DeleteFamilyById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFamily_InvalidFamily_ReturnsNotFound()
        {
            // Arrange
            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(await Task.FromResult<FamilyDto>(null));

            // Act 
            var result = await _controller.DeleteFamilyById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFamily_Exception_ReturnsServerError()
        {
            // Arrange
            int id = 1;

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.DeleteFamilyById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetFamiliesWithMembers_HasMembers_ReturnsOk()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembers())
                .ReturnsAsync(new List<FamilyWithMemberDto> { new FamilyWithMemberDto() });

            // Act 
            var result = await _controller.GetFamiliesWithMembers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembers_HasNoMembers_ReturnsNoContent()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembers())
                .ReturnsAsync(new List<FamilyWithMemberDto> { });

            // Act 
            var result = await _controller.GetFamiliesWithMembers();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembers_Exception_ReturnsServerError()
        {
            // Arrange
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembers())
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetFamiliesWithMembers();

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetFamiliesWithMembersById_FamilyExists_ReturnsOk()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersById(id))
                .ReturnsAsync(new FamilyWithMemberDto());

            // Act 
            var result = await _controller.GetFamiliesWithMembersById(id);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembersById_FamilyDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersById(id))
                .ReturnsAsync(await Task.FromResult<FamilyWithMemberDto>(null));

            // Act 
            var result = await _controller.GetFamiliesWithMembersById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembersById_Exception_ReturnsServerError()
        {
            // Arrange
            int id = 1;
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersById(id))
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetFamiliesWithMembersById(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetFamiliesWithMembersByName_FamilyExists_ReturnsOk()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersByName(name))
                .ReturnsAsync(new FamilyWithMemberDto());

            // Act 
            var result = await _controller.GetFamiliesWithMembersByName(name);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembersByName_FamilyDoesNotExists_ReturnsNotfound()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersByName(name))
                .ReturnsAsync(await Task.FromResult<FamilyWithMemberDto>(null));

            // Act 
            var result = await _controller.GetFamiliesWithMembersByName(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetFamiliesWithMembersByName_Exception_ReturnsServerError()
        {
            // Arrange
            string name = "test";
            _mockFamilyService.Setup(service => service.GetFamilyWithFamilyMembersByName(name))
                .Throws(new Exception());

            // Act 
            var result = await _controller.GetFamiliesWithMembersByName(name);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
