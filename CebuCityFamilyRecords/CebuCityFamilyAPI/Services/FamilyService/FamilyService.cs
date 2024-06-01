using AutoMapper;
using CebuCityFamilyAPI.Dtos.FamilyDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.FamilyRepository;

namespace CebuCityFamilyAPI.Services.FamilyService
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _repository;
        private readonly IMapper _mapper;
        public FamilyService(IFamilyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Register(int BId, FamilyCreationDto family)
        {
            var model = new Family
            {
                Name = family.Name,
                Sitio = family.Sitio,
                BId = BId,
                Barangay = new Barangay
                {
                    Id = BId
                }
            };
            return await _repository.CreateFamily(model);
        }

        public async Task<FamilyDto> CreateFamily(FamilyCreationDto familyToCreate)
        {
            var familyModel = _mapper.Map<Family>(familyToCreate);

            familyModel.Id = await _repository.CreateFamily(familyModel);

            return _mapper.Map<FamilyDto>(familyModel);

        }

        public async Task DeleteFamilyById(int id)
        {
            await _repository.DeleteFamilyById(id);
        }

        public async Task<IEnumerable<FamilyDto>> GetAllFamilies()
        {
            var familyModels = await _repository.GetAllFamilies();

            return _mapper.Map<IEnumerable<FamilyDto>>(familyModels);
        }

        public async Task<IEnumerable<FamilyDto>> GetFamilyByBarangayId(int id)
        {
            var familyModels = await _repository.GetFamilyByBarangayId(id);

            if (familyModels == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<FamilyDto>>(familyModels);
        }

        public async Task<FamilyDto?> GetFamilyById(int id)
        {
            var familyModel = await _repository.GetFamilyById(id);

            if (familyModel == null)
            {
                return null;
            }

            return _mapper.Map<FamilyDto>(familyModel);
        }

        public async Task<IEnumerable<FamilyDto>> GetFamilyByBarangayName(string name)
        {
            var familyModels = await _repository.GetFamilyByBarangayName(name);

            if (familyModels == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<FamilyDto>>(familyModels);
        }

        public async Task<FamilyDto> UpdateFamily(int id, FamilyUpdationDto familyToUpdate)
        {
            var familyModel = _mapper.Map<Family>(familyToUpdate);

            familyModel.Id = id;

            var updatedFamily = await _repository.UpdateFamily(familyModel);

            return _mapper.Map<FamilyDto>(updatedFamily);
        }

        public async Task<IEnumerable<FamilyWithMemberDto>> GetFamilyWithFamilyMembers()
        {
            var model = await _repository.GetFamilyWithFamilyMembers();

            if (model == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<FamilyWithMemberDto>>(model);
        }

        public async Task<FamilyWithMemberDto?> GetFamilyWithFamilyMembersById(int id)
        {
            var model = await _repository.GetFamilyWithFamilyMembersById(id);
            if (model == null)
            {
                return null;
            }
            return _mapper.Map<FamilyWithMemberDto>(model);
        }

        public async Task<FamilyWithMemberDto> GetFamilyWithFamilyMembersByName(string name)
        {
            var model = await _repository.GetFamilyWithFamilyMembersByName(name);
            if (model == null)
            {
                return null;
            }
            return _mapper.Map<FamilyWithMemberDto>(model);
        }
    }
}
