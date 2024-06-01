using AutoMapper;
using CebuCityFamilyAPI.Dtos.BarangayDto;
using CebuCityFamilyAPI.Models;
using CebuCityFamilyAPI.Repositories.BarangayRepository;

namespace CebuCityFamilyAPI.Services.BarangayService
{
    public class BarangayService : IBarangayService
    {
        private readonly IBarangayRepository _repository;
        private readonly IMapper _mapper;

        public BarangayService(IBarangayRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BarangayDto> CreateBarangay(BarangayCreationDto barangayToCreate)
        {
            var barangayModel = _mapper.Map<Barangay>(barangayToCreate);

            barangayModel.Id = await _repository.CreateBarangay(barangayModel);

            return _mapper.Map<BarangayDto>(barangayModel);
        }

        public async Task<IEnumerable<BarangayDto>> GetAllBarangays()
        {
            var barangayModels = await _repository.GetAllBarangays();

            return _mapper.Map<IEnumerable<BarangayDto>>(barangayModels);
        }
        public async Task<BarangayDto?> GetBarangayById(int id)
        {
            var barangayModel = await _repository.GetBarangayById(id);

            if (barangayModel == null)
            {
                return null;
            }

            return _mapper.Map<BarangayDto>(barangayModel);
        }

        public async Task<BarangayDto?> GetBarangayByName(string name)
        {
            var barangayModel = await _repository.GetBarangayByName(name);

            if (barangayModel == null)
            {
                return null;
            }

            return _mapper.Map<BarangayDto>(barangayModel);
        }

        public async Task<BarangayDto> UpdateBarangay(int id, BarangayUpdationDto barangayToUpdate)
        {
            var barangayModel = _mapper.Map<Barangay>(barangayToUpdate);

            barangayModel.Id = id;

            var updatedBarangay = await _repository.UpdateBarangay(barangayModel);

            return _mapper.Map<BarangayDto>(updatedBarangay);
        }

        public async Task<BarangayDto> UpdateBarangayByName(string name, BarangayUpdationDto barangayToUpdate)
        {
            var barangayModel = _mapper.Map<Barangay>(barangayToUpdate);
            var updatedBarangay = await _repository.UpdateBarangayByName(name, barangayModel);
            return _mapper.Map<BarangayDto>(updatedBarangay);
        }

        public async Task<bool> DeleteBarangay(string name)
        {
            return await _repository.DeleteBarangay(name) > 0;
        }

        public async Task<int> CountBarangayName(string name)
        {
            return await _repository.CountBarangayName(name);
        }

        public async Task<int> GetPopulationInBarangay(int barangayId)
        {
            return await _repository.GetPopulationInBarangay(barangayId);
        }
    }
}
