using AutoMapper;
using CebuCityFamilyAPI.Dtos.AccountDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.AccountRepository;

namespace CebuCityFamilyAPI.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccount(AccountCreationDto accountDto)
        {
            if (accountDto == null)
            {
                return null;
            }
            var accountModel = _mapper.Map<Account>(accountDto);
            var accountId = await _repository.CreateAccount(accountModel);

            if (accountId < 0)
            {
                return null;
            }

            accountModel.Id = accountId;

            return _mapper.Map<AccountDto>(accountModel);
        }
        public async Task<AccountDto?> GetAccountById(int id)
        {
            var accountModel = await _repository.GetAccountById(id);

            if (accountModel == null)
            {
                return null;
            }
            return _mapper.Map<AccountDto>(accountModel);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccount()
        {
            var accountModels = await _repository.GetAllAccounts();

            return _mapper.Map<IEnumerable<AccountDto>>(accountModels);
        }

        public async Task<int?> Login(string emailAddress, string password)
        {
            var userId = await _repository.Login(emailAddress, password);

            if (userId == null)
            {
                return null;
            }
            return userId;
        }

        public async Task<bool> DeleteAccount(int id)
        {
            return await _repository.DeleteAccount(id);
        }
    }
}
