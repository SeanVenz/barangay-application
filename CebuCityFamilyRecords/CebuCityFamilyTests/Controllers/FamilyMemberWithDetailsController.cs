using CebuCityFamilyAPI.Controllers;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Services.FamilyMembersService;
using CebuCityFamilyAPI.Services.FamilyService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CebuCityFamilyAPITests.Controllers
{
    public class FamilyMemberWithDetailsControllerTests
    {
        private readonly FamilyMemberWithDetailsController _controller;
        private readonly Mock<IFamilyMembersService> _mockFamilyMemberService;
        private readonly Mock<IFamilyService> _mockFamilyService;
        private readonly Mock<ILogger<FamilyMemberWithDetailsController>> _mockLogger;

        public FamilyMemberWithDetailsControllerTests()
        {

            _mockFamilyMemberService = new Mock<IFamilyMembersService>();
            _mockFamilyService = new Mock<IFamilyService>();
            _mockLogger = new Mock<ILogger<FamilyMemberWithDetailsController>>();

            _controller = new FamilyMemberWithDetailsController(_mockFamilyMemberService.Object, _mockFamilyService.Object, _mockLogger.Object);

        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_HasFamilyMember_ReturnsOk()
        {
            //Arrange
            _mockFamilyMemberService.Setup(service => service.GetAllFamilyMemberWithDetails())
                .ReturnsAsync(new List<FamilyMemberWithDetailsDto> { new FamilyMemberWithDetailsDto() });

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetails();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_HasNoFamilyMember_ReturnsNoContent()
        {
            //Arrange
            _mockFamilyMemberService.Setup(service => service.GetAllFamilyMemberWithDetails())
                .ReturnsAsync(new List<FamilyMemberWithDetailsDto>());

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetails();

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetails_Exception_ReturnsServerError()
        {
            //Arrange
            _mockFamilyMemberService.Setup(service => service.GetAllFamilyMemberWithDetails())
                .Throws(new Exception());

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetails();

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsById_HasFamilyMember_ReturnsOk()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetailsDto());

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetailsById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsById_HasNoFamilyMember_ReturnsNotFound()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetailsDto?)null);

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetailsById(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsById_Exception_ReturnsServerError()
        {
            //Arrange
            int id = 1;
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetailsById(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsByName_HasFamilyMember_ReturnsOk()
        {
            //Arrange
            string name = "john";
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsByName(name))
                .ReturnsAsync(new List<FamilyMemberWithDetailsDto> { new FamilyMemberWithDetailsDto() });

            //Act
            var result = await _controller.GetAllFamilyMemberWithDetailsByName(name);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsByName_HasNoFamilyMember_ReturnsNotFound()
        {
            // Arrange
            string name = "john";
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsByName(name))
                .ReturnsAsync((List<FamilyMemberWithDetailsDto?>)null);

            // Act
            var result = await _controller.GetAllFamilyMemberWithDetailsByName(name);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAllFamilyMemberWithDetailsByName_Exception_ReturnsServerError()
        {
            // Arrange
            string name = "john";
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsByName(name))
                .Throws(new Exception());

            // Act
            var result = await _controller.GetAllFamilyMemberWithDetailsByName(name);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_ValidDetails_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var familyMember = new FamilyMemberWithDetailsCreationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "01-22-2001",
                Gender = "Male",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());
            _mockFamilyMemberService.Setup(service => service.CreateFamilyMemberWithDetails(id, familyMember))
                .ReturnsAsync(new FamilyMemberWithDetailsDto());

            //Act
            var result = await _controller.CreateFamilyMemberWithDetails(id, familyMember);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_NoFamily_ReturnsNotFound()
        {
            //Arrange
            int id = 1;
            var familyMember = new FamilyMemberWithDetailsCreationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "01-22-2001",
                Gender = "Male",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync((FamilyDto?)null);

            //Act
            var result = await _controller.CreateFamilyMemberWithDetails(id, familyMember);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_ExceptionInNoId_ReturnsServerErroor()
        {
            //Arrange
            int id = 1;
            var familyMember = new FamilyMemberWithDetailsCreationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "01-22-2001",
                Gender = "Male",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.CreateFamilyMemberWithDetails(id, familyMember);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateFamilyMemberWithDetails_ExceptionInCreating_ReturnsServerErroor()
        {
            //Arrange
            int id = 1;
            var familyMember = new FamilyMemberWithDetailsCreationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "01-22-2001",
                Gender = "Male",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "Catholic"
            };

            _mockFamilyService.Setup(service => service.GetFamilyById(id))
                .ReturnsAsync(new FamilyDto());
            _mockFamilyMemberService.Setup(service => service.CreateFamilyMemberWithDetails(id, familyMember))
                .Throws(new Exception());

            //Act
            var result = await _controller.CreateFamilyMemberWithDetails(id, familyMember);

            // Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task UpdateFamilyMember_MemberExists_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var updateMember = new FamilyMemberWithDetailsUpdationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Female",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "INC"
            };

            _mockFamilyMemberService.Setup(service => service.UpdateFamilyMemberWithDetails(id, updateMember))
                .ReturnsAsync(true);
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetailsDto());

            //Act
            var result = await _controller.UpdateFamilyMember(id, updateMember);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFamilyMember_MemberDoesNotExists_ReturnsNotFound()
        {
            //Arrange
            int id = 1;
            var updateMember = new FamilyMemberWithDetailsUpdationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Female",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "INC"
            };

            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetailsDto?)null);

            //Act
            var result = await _controller.UpdateFamilyMember(id, updateMember);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task UpdateFamilyMember_Exception_ReturnsServerError()
        {
            //Arrange
            int id = 1;
            var updateMember = new FamilyMemberWithDetailsUpdationDto
            {
                LastName = "Doe",
                FirstName = "John",
                Age = 23,
                MaritalStatus = "Single",
                BirthDate = "12-23-2001",
                Gender = "Female",
                Occupation = "Student",
                ContactNo = "+639123456789",
                Religion = "INC"
            };

            _mockFamilyMemberService.Setup(service => service.UpdateFamilyMemberWithDetails(id, updateMember))
                .Throws(new Exception());
            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.UpdateFamilyMember(id, updateMember);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteFamilyMember_HasMember_ReturnsOk()
        {
            //Arrange
            int id = 1;

            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetailsDto());
            _mockFamilyMemberService.Setup(service => service.DeleteFamilyMemberWithDetails(id))
                .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteFamilyMember(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFamilyMember_HasNoMember_ReturnsNotFound()
        {
            //Arrange
            int id = 1;

            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync((FamilyMemberWithDetailsDto?)null);

            //Act
            var result = await _controller.DeleteFamilyMember(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteFamilyMember_Exception_ReturnsServerError()
        {
            //Arrange
            int id = 1;

            _mockFamilyMemberService.Setup(service => service.GetFamilyMemberWithDetailsById(id))
                .ReturnsAsync(new FamilyMemberWithDetailsDto());
            _mockFamilyMemberService.Setup(service => service.DeleteFamilyMemberWithDetails(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.DeleteFamilyMember(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
