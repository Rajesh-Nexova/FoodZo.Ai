using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Services.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class HalfYearlyReminderService : IHalfYearlyReminderService
    {
        private readonly IHalfYearlyReminderRepository _repository;
        private readonly IHalfYearlyReminderMapper _mapper;

        public HalfYearlyReminderService(IHalfYearlyReminderRepository repository, IHalfYearlyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<HalfYearlyReminderDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new HalfYearlyReminderDTO
            {
                Id = e.Id,
                ReminderId = e.ReminderId,
                Day = e.Day,
                Month = e.Month,
                Quarter = e.Quarter,
                IsActive = e.IsActive
            }).ToList();
        }

        public async Task<HalfYearlyReminderDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : new HalfYearlyReminderDTO
            {
                Id = entity.Id,
                ReminderId = entity.ReminderId,
                Day = entity.Day,
                Month = entity.Month,
                Quarter = entity.Quarter,
                IsActive = entity.IsActive
            };
        }

        public async Task<HalfYearlyReminderDTO> CreateAsync(HalfYearlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var created = await _repository.AddAsync(entity);
            return new HalfYearlyReminderDTO
            {
                Id = created.Id,
                ReminderId = created.ReminderId,
                Day = created.Day,
                Month = created.Month,
                Quarter = created.Quarter,
                IsActive = created.IsActive
            };
        }

        public async Task<HalfYearlyReminderDTO> UpdateAsync(HalfYearlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var updated = await _repository.UpdateAsync(entity);
            return new HalfYearlyReminderDTO
            {
                Id = updated.Id,
                ReminderId = updated.ReminderId,
                Day = updated.Day,
                Month = updated.Month,
                Quarter = updated.Quarter,
                IsActive = updated.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
