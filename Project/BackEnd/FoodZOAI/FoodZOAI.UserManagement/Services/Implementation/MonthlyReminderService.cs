using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Services.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class MonthlyReminderService : IMonthlyReminderService
    {
        private readonly IMonthlyReminderRepository _repository;
        private readonly IMonthlyReminderMapper _mapper;

        public MonthlyReminderService(IMonthlyReminderRepository repository, IMonthlyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<MonthlyReminderDTO>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            // Manual mapping back to DTO
            return data.Select(e => new MonthlyReminderDTO
            {
                Id = e.Id,
                Subject = e.Subject,
                Message = e.Message,
                Frequency = e.Frequency,
                DayOfMonth = e.DayOfMonth,
                StartDate = e.StartDate,
                IsRepeated = e.IsRepeated,
                IsEmailNotification = e.IsEmailNotification,
                IsActive = e.IsActive,
                CreatedByUser = e.CreatedByUser
            }).ToList();
        }

        public async Task<MonthlyReminderDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : new MonthlyReminderDTO
            {
                Id = entity.Id,
                Subject = entity.Subject,
                Message = entity.Message,
                Frequency = entity.Frequency,
                DayOfMonth = entity.DayOfMonth,
                StartDate = entity.StartDate,
                IsRepeated = entity.IsRepeated,
                IsEmailNotification = entity.IsEmailNotification,
                IsActive = entity.IsActive,
                CreatedByUser = entity.CreatedByUser
            };
        }

        public async Task<MonthlyReminderDTO> CreateAsync(MonthlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var created = await _repository.AddAsync(entity);
            dto.Id = created.Id;
            return dto;
        }

        public async Task<MonthlyReminderDTO> UpdateAsync(MonthlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var updated = await _repository.UpdateAsync(entity);
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
