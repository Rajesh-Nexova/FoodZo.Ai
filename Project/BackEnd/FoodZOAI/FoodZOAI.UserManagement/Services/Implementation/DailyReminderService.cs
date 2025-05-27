using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class DailyReminderService : IDailyReminderService
    {
        private readonly IDailyReminderRepository _repository;
        private readonly IDailyReminderMapper _mapper;

        public DailyReminderService(IDailyReminderRepository repository, IDailyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<DailyReminderDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtoList = new List<DailyReminderDTO>();
            foreach (var entity in entities)
            {
                dtoList.Add(new DailyReminderDTO
                {
                    Id = entity.Id,
                    ReminderId = entity.ReminderId,
                    DayOfWeek = entity.DayOfWeek,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt,
                    DeletedAt = entity.DeletedAt,
                    IsActive = entity.IsActive
                });
            }
            return dtoList;
        }

        public async Task<DailyReminderDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : new DailyReminderDTO
            {
                Id = entity.Id,
                ReminderId = entity.ReminderId,
                DayOfWeek = entity.DayOfWeek,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                DeletedAt = entity.DeletedAt,
                IsActive = entity.IsActive
            };
        }

        public async Task<DailyReminderDTO> CreateAsync(DailyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var created = await _repository.AddAsync(entity);
            return new DailyReminderDTO
            {
                Id = created.Id,
                ReminderId = created.ReminderId,
                DayOfWeek = created.DayOfWeek,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,
                DeletedAt = created.DeletedAt,
                IsActive = created.IsActive
            };
        }

        public async Task<DailyReminderDTO> UpdateAsync(DailyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var updated = await _repository.UpdateAsync(entity);
            return new DailyReminderDTO
            {
                Id = updated.Id,
                ReminderId = updated.ReminderId,
                DayOfWeek = updated.DayOfWeek,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt,
                DeletedAt = updated.DeletedAt,
                IsActive = updated.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
