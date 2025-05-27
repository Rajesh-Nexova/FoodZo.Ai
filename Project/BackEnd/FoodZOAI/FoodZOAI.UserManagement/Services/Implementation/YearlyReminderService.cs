using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services
{
    public class YearlyReminderService : IYearlyReminderService
    {
        private readonly IYearlyReminderRepository _repository;
        private readonly IYearlyReminderMapper _mapper;

        public YearlyReminderService(IYearlyReminderRepository repository, IYearlyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<YearlyReminderDTO>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();
            return data.Select(x => new YearlyReminderDTO
            {
                Id = x.Id,
                ReminderId = x.ReminderId,
                Day = x.Day,
                Month = x.Month,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<YearlyReminderDTO?> GetByIdAsync(int id)
        {
            var reminder = await _repository.GetByIdAsync(id);
            if (reminder == null) return null;

            return new YearlyReminderDTO
            {
                Id = reminder.Id,
                ReminderId = reminder.ReminderId,
                Day = reminder.Day,
                Month = reminder.Month,
                IsActive = reminder.IsActive
            };
        }

        public async Task<YearlyReminderDTO> CreateAsync(YearlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var created = await _repository.CreateAsync(entity);

            dto.Id = created.Id;
            return dto;
        }

        public async Task<YearlyReminderDTO> UpdateAsync(YearlyReminderDTO dto)
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
