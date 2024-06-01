using AutoMapper;
using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.AccountRepository;
using CebuCityFamilyAPI.Services.AccountService;
using Moq;

namespace CebuCityFamilyAPITests.Services
{
    public class AccountServiceTests
    {
        private readonly Mock<IAccountRepository> _mockAccountRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new AccountService(_mockAccountRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateAccount_ValidAccount_CreatesAccount()
        {
            // Arrange
            var accountDto = new AccountCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            var account = new Account
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            var accountDtoReturn = new AccountDto
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            _mockMapper.Setup(mapper => mapper.Map<Account>(accountDto))
                .Returns(account);
            _mockAccountRepository.Setup(repo => repo.CreateAccount(account))
                .ReturnsAsync(1);
            _mockMapper.Setup(mapper => mapper.Map<AccountDto>(It.Is<Account>(x => x.Id == 1)))
                .Returns(accountDtoReturn);

            // Act
            var result = await _service.CreateAccount(accountDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accountDtoReturn, result);
        }

        [Fact]
        public async Task CreateAccount_RepositoryFails_ReturnsError()
        {
            // Arrange
            var accountCreationDto = new AccountCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            var account = new Account
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            _mockMapper.Setup(mapper => mapper.Map<Account>(accountCreationDto))
                .Returns(account);
            _mockAccountRepository.Setup(repo => repo.CreateAccount(account))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.CreateAccount(accountCreationDto));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task CreateAccount_RepositoryReturnsErrorCode_ReturnsNull()
        {
            // Arrange
            var accountDto = new AccountCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            var account = new Account
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password1@"
            };

            _mockMapper.Setup(mapper => mapper.Map<Account>(accountDto)).Returns(account);
            _mockAccountRepository.Setup(repo => repo.CreateAccount(account)).ReturnsAsync(-2); // Simulate error code 2.

            // Act
            var result = await _service.CreateAccount(accountDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAccountById_ExistingId_ReturnsAccountDto()
        {
            // Arrange
            int id = 1;
            var accountModel = new Account
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "123456",
                Password = "Password@123"
            };
            var accountDto = new AccountDto
            {
                Id = id,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "123456",
                Password = "Password@123"
            };
            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .ReturnsAsync(accountModel);
            _mockMapper.Setup(mapper => mapper.Map<AccountDto>(accountModel))
                .Returns(accountDto);

            // Act
            var result = await _service.GetAccountById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AccountDto>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetAccountById_NonExistingId_ReturnsNull()
        {
            // Arrange
            int id = 1;
            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .ReturnsAsync((Account?)null);

            // Act
            var result = await _service.GetAccountById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAccountById_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            int id = 1;
            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .Throws(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAccountById(id));

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task GetAllAccounts_WhenCalled_ReturnsAllAccounts()
        {
            // Arrange
            var accountModels = new List<Account>
            {
                new Account { Id = 1, FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@example.com", PhoneNumber = "+639123456789", GovernmentIdNumber = "123456", Password = "Password@123" },
                new Account { Id = 2, FirstName = "Jane", LastName = "Doe", EmailAddress = "jane.doe@example.com", PhoneNumber = "+639123456789", GovernmentIdNumber = "654321", Password = "Password@123" }
            };
            var accountDtos = new List<AccountDto>
            {
                new AccountDto { Id = 1, FirstName = "John", LastName = "Doe", EmailAddress = "john.doe@example.com", PhoneNumber = "+639123456789", GovernmentIdNumber = "123456", Password = "Password@123" },
                new AccountDto { Id = 2, FirstName = "Jane", LastName = "Doe", EmailAddress = "jane.doe@example.com", PhoneNumber = "+639123456789", GovernmentIdNumber = "654321", Password = "Password@123" }
            };
            _mockAccountRepository.Setup(repo => repo.GetAllAccounts())
                .ReturnsAsync(accountModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<AccountDto>>(accountModels))
                .Returns(accountDtos);

            // Act
            var result = await _service.GetAllAccount();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AccountDto>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAccounts_WhenCalled_ReturnsEmpty()
        {
            // Arrange
            var accountModels = new List<Account>();
            var accountDtos = new List<AccountDto>();

            _mockAccountRepository.Setup(repo => repo.GetAllAccounts())
                .ReturnsAsync(accountModels);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<AccountDto>>(accountModels))
                .Returns(accountDtos);

            // Act
            var result = await _service.GetAllAccount();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<AccountDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllAccounts_WhenRepositoryThrowsException_ThrowsException()
        {
            // Arrange
            _mockAccountRepository.Setup(repo => repo.GetAllAccounts())
                .ThrowsAsync(new Exception("Database connection error"));

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.GetAllAccount());

            // Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUserId()
        {
            // Arrange
            string emailAddress = "john.doe@example.com";
            string password = "Password@123";
            int expectedUserId = 1;

            _mockAccountRepository.Setup(repo => repo.Login(emailAddress, password)).ReturnsAsync(expectedUserId);

            // Act
            int? userId = await _service.Login(emailAddress, password);

            // Assert
            Assert.NotNull(userId);
            Assert.Equal(expectedUserId, userId);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            string emailAddress = "john.doe@example.com";
            string password = "IncorrectPassword";

            _mockAccountRepository.Setup(repo => repo.Login(emailAddress, password))
                .ReturnsAsync((int?)null);

            // Act
            int? userId = await _service.Login(emailAddress, password);

            // Assert
            Assert.Null(userId);
        }

        [Fact]
        public async Task Login_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            string emailAddress = "john.doe@example.com";
            string password = "IncorrectPassword";

            _mockAccountRepository.Setup(repo => repo.Login(emailAddress, password))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.Login(emailAddress, password));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }

        [Fact]
        public async Task DeleteAccount_ValidId_ReturnsTrue()
        {
            // Arrange
            int id = 1;

            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .ReturnsAsync(new Account());
            _mockAccountRepository.Setup(repo => repo.DeleteAccount(id))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAccount(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAccount_InvalidId_ReturnsFalse()
        {
            // Arrange
            int id = -1;

            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .ReturnsAsync((Account?)null);
            _mockAccountRepository.Setup(repo => repo.DeleteAccount(id))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAccount(id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAccount_WhenRepositoryFails_ReturnsError()
        {
            // Arrange
            int id = -1;

            _mockAccountRepository.Setup(repo => repo.GetAccountById(id))
                .Throws(new Exception("Database connection error"));
            _mockAccountRepository.Setup(repo => repo.DeleteAccount(id))
                .Throws(new Exception("Database connection error"));

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _service.DeleteAccount(id));

            //Assert
            Assert.Equal("Database connection error", exception.Message);
        }
    }
}
