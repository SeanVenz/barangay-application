using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyMembersDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.FamilyMembersRepository;

namespace CebuCityFamilyAPI.Services.FamilyMembersService
{
    public class FamilyMembersService : IFamilyMembersService
    {
        private readonly IFamilyMembersRepository _repository;
        private readonly IMapper _mapper;

        public FamilyMembersService(IFamilyMembersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FamilyMembersDto>> GetFamilyMembersById(int id)
        {
            var familyMembersModels = await _repository.GetFamilyMembersById(id);

            return _mapper.Map<IEnumerable<FamilyMembersDto>>(familyMembersModels);
        }

        public async Task<IEnumerable<FamilyMembersDto>> GetFamilyMembersByName(string name)
        {
            var familyMembersModels = await _repository.GetFamilyMembersByName(name);

            return _mapper.Map<IEnumerable<FamilyMembersDto>>(familyMembersModels);
        }

        public async Task<IEnumerable<FamilyMemberWithDetailsDto>> GetAllFamilyMemberWithDetails()
        {
            var model = await _repository.GetAllFamilyMemberWithDetails();

            return _mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(model);
        }

        public async Task<FamilyMemberWithDetailsDto> GetFamilyMemberWithDetailsById(int id)
        {
            var model = await _repository.GetFamilyMemberWithDetailsById(id);

            if (model == null)
            {
                return null;
            }

            return _mapper.Map<FamilyMemberWithDetailsDto>(model);
        }

        public async Task<IEnumerable<FamilyMemberWithDetailsDto>> GetFamilyMemberWithDetailsByName(string name)
        {
            var models = await _repository.GetFamilyMemberWithDetailsByName(name);
            if (models == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<FamilyMemberWithDetailsDto>>(models);
        }

        public async Task<FamilyMemberWithDetailsDto> CreateFamilyMemberWithDetails(int id, FamilyMemberWithDetailsCreationDto familyMember)
        {
            var model = _mapper.Map<FamilyMemberWithDetails>(familyMember);

            model.Id = await _repository.CreateFamilyMemberWithDetails(id, model);

            model.FId = id;

            return _mapper.Map<FamilyMemberWithDetailsDto>(model);
        }

        public async Task<bool> UpdateFamilyMemberWithDetails(int id, FamilyMemberWithDetailsUpdationDto familyMember)
        {
            var model = _mapper.Map<FamilyMemberWithDetails>(familyMember);
            model.Id = id;
            return await _repository.UpdateFamilyMemberWithDetails(model);
        }

        public async Task<bool> DeleteFamilyMemberWithDetails(int id)
        {
            return await _repository.DeleteFamilyMemberWithDetails(id);
        }

    }
}
