using CebuCityFamilyAPI.Controllers;
using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Dtos.LoginDto;
using CebuCityFamilyAPI.Services.AccountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CebuCityFamilyAPITests.Controllers
{
    public class AccountsControllerTests
    {
        private readonly AccountsController _controller;
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly Mock<ILogger<AccountsController>> _mockLogger;

        public AccountsControllerTests()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mockLogger = new Mock<ILogger<AccountsController>>();

            _controller = new AccountsController(_mockAccountService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAccounts_HasAccount_ReturnsOk()
        {
            //Arrange
            _mockAccountService.Setup(service => service.GetAllAccount())
                .ReturnsAsync(new List<AccountDto> { new AccountDto() });

            //Act
            var result = await _controller.GetAllAccount();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllAccounts_HasNoAccount_ReturnsNoContent()
        {
            //Arrange
            _mockAccountService.Setup(service => service.GetAllAccount())
                .ReturnsAsync(new List<AccountDto>());

            //Act
            var result = await _controller.GetAllAccount();

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CreateAccount_ReturnsBadRequest_WhenDetailsExists()
        {
            // Arrange
            var accountDto = new AccountCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            _mockAccountService.Setup(service => service.CreateAccount(accountDto))
                .ReturnsAsync((AccountDto?)null);

            // Act
            var result = await _controller.CreateAccount(accountDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("An account with the same email address or phone number or government ID number already exists.", badRequestResult.Value);
        }

        [Fact]
        public async Task GetAllAccounts_Exception_ReturnsServerError()
        {
            //Arrange
            _mockAccountService.Setup(service => service.GetAllAccount())
                .Throws(new Exception());

            //Act
            var result = await _controller.GetAllAccount();

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task GetAccountById_HasAccount_ReturnsOk()
        {
            //Arrange
            int id = 1;
            _mockAccountService.Setup(service => service.GetAccountById(id))
                .ReturnsAsync(new AccountDto());

            //Act
            var result = await _controller.GetAccountById(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAccountById_HasNoAccount_ReturnsNotFound()
        {
            //Arrange
            int id = 1;
            _mockAccountService.Setup(service => service.GetAccountById(id))
                .ReturnsAsync((AccountDto?)null);

            //Act
            var result = await _controller.GetAccountById(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task GetAccountById_Exception_ReturnsServerError()
        {
            //Arrange
            int id = 1;
            _mockAccountService.Setup(service => service.GetAccountById(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.GetAccountById(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateAccount_ValidAccount_ReturnsOk()
        {
            //Arrange
            var account = new AccountCreationDto()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            _mockAccountService.Setup(service => service.CreateAccount(account))
                .ReturnsAsync(new AccountDto());

            //Act
            var result = await _controller.CreateAccount(account);

            //Assert
            Assert.IsType<CreatedAtRouteResult>(result);
        }

        [Fact]
        public async Task CreateAccount_HasExistingDetails_ReturnsBadRequest()
        {
            //Arrange
            var account = new AccountCreationDto()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            _mockAccountService.Setup(service => service.CreateAccount(account))
                .ReturnsAsync((AccountDto?)null);

            //Act
            var result = await _controller.CreateAccount(account);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task CreateAccount_Exception_ReturnsServerError()
        {
            //Arrange
            var account = new AccountCreationDto()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            _mockAccountService.Setup(service => service.CreateAccount(account))
                .Throws(new Exception());

            //Act
            var result = await _controller.CreateAccount(account);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Login_WithCorrectCredentials_ReturnsOkResult()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                Email = "test@test.com",
                Password = "Password.1"
            };
            _mockAccountService.Setup(service => service.Login(loginDto.Email, loginDto.Password)).ReturnsAsync(1);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Logged In", okResult.Value);
        }

        [Fact]
        public async Task Login_IncorrectCredentials_ReturnsUnauthorizedResult()
        {
            // Arrange
            var loginDto = new LoginDto()
            {
                Email = "test@test.com",
                Password = "Password.1"
            };

            _mockAccountService.Setup(service => service.Login(loginDto.Email, loginDto.Password))
                .ReturnsAsync((int?)null);

            //Act
            var result = await _controller.Login(loginDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task Login_Exception_ReturnsServerError()
        {
            //Arrange
            var loginDto = new LoginDto()
            {
                Email = "test@test.com",
                Password = "Password.1"
            };

            _mockAccountService.Setup(service => service.Login(loginDto.Email, loginDto.Password))
                .Throws(new Exception());

            //Act
            var result = await _controller.Login(loginDto);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }

        [Fact]
        public async Task DeleteAccount_HasAccount_ReturnsOk()
        {
            //Arrange
            int id = 1;
            var account = new AccountDto()
            {
                Id = id,
                FirstName = "Test",
                LastName = "Test",
                EmailAddress = "test@test.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "12-34-567",
                Password = "Password.1"
            };

            _mockAccountService.Setup(service => service.GetAccountById(id))
                .ReturnsAsync(account);
            _mockAccountService.Setup(service => service.DeleteAccount(id))
                .ReturnsAsync(true);

            //Act
            var result = await _controller.DeleteAccount(id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Account with ID = {id} is successfully deleted.", okResult.Value);
        }

        [Fact]
        public async Task DeleteAccount_HasNoAccount_ReturnsNotFound()
        {
            //Arrange
            int id = 1;

            _mockAccountService.Setup(service => service.GetAccountById(id))
                .ReturnsAsync((AccountDto?)null);

            //Act
            var result = await _controller.DeleteAccount(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteAccount_Exception_ReturnsServerError()
        {
            //Arrange
            int id = 1;

            _mockAccountService.Setup(service => service.GetAccountById(id))
                .Throws(new Exception());

            //Act
            var result = await _controller.DeleteAccount(id);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, ((ObjectResult)result).StatusCode);
        }
    }
}
