using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class WeeklyReminderService : IWeeklyReminderService
    {
        private readonly IWeeklyReminderRepository _repository;
        private readonly IWeeklyReminderMapper _mapper;

        public WeeklyReminderService(IWeeklyReminderRepository repository, IWeeklyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<WeeklyReminderDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var dtos = new List<WeeklyReminderDTO>();

            foreach (var entity in entities)
            {
                dtos.Add(new WeeklyReminderDTO
                {
                    Id = entity.Id,
                    Subject = entity.Subject,
                    Message = entity.Message,
                    Frequency = entity.Frequency,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate,
                    DayOfWeek = entity.DayOfWeek,
                    IsRepeated = entity.IsRepeated,
                    IsEmailNotification = entity.IsEmailNotification,
                    IsActive = entity.IsActive
                });
            }

            return dtos;
        }

        public async Task<WeeklyReminderDTO?> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new WeeklyReminderDTO
            {
                Id = entity.Id,
                Subject = entity.Subject,
                Message = entity.Message,
                Frequency = entity.Frequency,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                DayOfWeek = entity.DayOfWeek,
                IsRepeated = entity.IsRepeated,
                IsEmailNotification = entity.IsEmailNotification,
                IsActive = entity.IsActive
            };
        }

        public async Task<WeeklyReminderDTO> CreateAsync(WeeklyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var created = await _repository.AddAsync(entity);

            return new WeeklyReminderDTO
            {
                Id = created.Id,
                Subject = created.Subject,
                Message = created.Message,
                Frequency = created.Frequency,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                DayOfWeek = created.DayOfWeek,
                IsRepeated = created.IsRepeated,
                IsEmailNotification = created.IsEmailNotification,
                IsActive = created.IsActive
            };
        }

        public async Task<WeeklyReminderDTO> UpdateAsync(WeeklyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var updated = await _repository.UpdateAsync(entity);

            return new WeeklyReminderDTO
            {
                Id = updated.Id,
                Subject = updated.Subject,
                Message = updated.Message,
                Frequency = updated.Frequency,
                StartDate = updated.StartDate,
                EndDate = updated.EndDate,
                DayOfWeek = updated.DayOfWeek,
                IsRepeated = updated.IsRepeated,
                IsEmailNotification = updated.IsEmailNotification,
                IsActive = updated.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
