using AutoMapper;
using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Mappings;
using CebuCityFamilyAPI.Models;

namespace CebuCityFamilyAPITests.Mapper
{
    public class AccountMappingTests
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _configuration;

        public AccountMappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountMappings>();
            });
            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void AccountCreationDtoMapsToAccount_MapsCorrectly_ReturnsMappedAccount()
        {
            // Arrange
            var accountCreationDto = new AccountCreationDto
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            // Act
            var account = _mapper.Map<Account>(accountCreationDto);

            // Assert
            Assert.Equal(account.FirstName, accountCreationDto.FirstName);
            Assert.Equal(account.LastName, accountCreationDto.LastName);
            Assert.Equal(account.EmailAddress, accountCreationDto.EmailAddress);
            Assert.Equal(account.PhoneNumber, accountCreationDto.PhoneNumber);
            Assert.Equal(account.GovernmentIdNumber, accountCreationDto.GovernmentIdNumber);
            Assert.Equal(account.Password, accountCreationDto.Password);
        }

        [Fact]
        public void AccountCreationDtoMapsToAccount_NullInput_ReturnsNull()
        {
            //Arrange
            AccountCreationDto familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<Account>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }

        [Fact]
        public void AccountMapsToAccountDto_MapsCorrectly_ReturnsMappedAccountDto()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "johndoe@gmail.com",
                PhoneNumber = "+639123456789",
                GovernmentIdNumber = "1234567890",
                Password = "Password.123"
            };

            // Act
            var accountDto = _mapper.Map<AccountDto>(account);

            // Assert
            Assert.Equal(accountDto.FirstName, account.FirstName);
            Assert.Equal(accountDto.LastName, account.LastName);
            Assert.Equal(accountDto.EmailAddress, account.EmailAddress);
            Assert.Equal(accountDto.PhoneNumber, account.PhoneNumber);
            Assert.Equal(accountDto.GovernmentIdNumber, account.GovernmentIdNumber);
            Assert.Equal(accountDto.Password, account.Password);
        }

        [Fact]
        public void AccountMapsToAccountDto_NullInput_ReturnsNull()
        {
            //Arrange
            Account familyMemberWithDetails = null;

            //Act
            var familyMember = _mapper.Map<AccountDto>(familyMemberWithDetails);

            //Assert
            Assert.Null(familyMember);
        }
    }
}
