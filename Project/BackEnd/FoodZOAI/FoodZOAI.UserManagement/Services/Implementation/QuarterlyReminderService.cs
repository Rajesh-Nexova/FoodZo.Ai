using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Services
{
    public class QuarterlyReminderService : IQuarterlyReminderService
    {
        private readonly IQuarterlyReminderRepository _repository;
        private readonly IQuarterlyReminderMapper _mapper;

        public QuarterlyReminderService(IQuarterlyReminderRepository repository, IQuarterlyReminderMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<QuarterlyReminderDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new QuarterlyReminderDTO
            {
                Id = e.Id,
                ReminderId = e.ReminderId,
                Day = e.Day,
                Month = e.Month,
                Quarter = e.Quarter,
                IsActive = e.IsActive
            }).ToList();
        }

        public async Task<QuarterlyReminderDTO?> GetByIdAsync(int id)
        {
            var e = await _repository.GetByIdAsync(id);
            return e == null ? null : new QuarterlyReminderDTO
            {
                Id = e.Id,
                ReminderId = e.ReminderId,
                Day = e.Day,
                Month = e.Month,
                Quarter = e.Quarter,
                IsActive = e.IsActive
            };
        }

        public async Task<QuarterlyReminderDTO> CreateAsync(QuarterlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var result = await _repository.AddAsync(entity);
            return new QuarterlyReminderDTO
            {
                Id = result.Id,
                ReminderId = result.ReminderId,
                Day = result.Day,
                Month = result.Month,
                Quarter = result.Quarter,
                IsActive = result.IsActive
            };
        }

        public async Task<QuarterlyReminderDTO> UpdateAsync(QuarterlyReminderDTO dto)
        {
            var entity = _mapper.MapToDomain(dto);
            var result = await _repository.UpdateAsync(entity);
            return new QuarterlyReminderDTO
            {
                Id = result.Id,
                ReminderId = result.ReminderId,
                Day = result.Day,
                Month = result.Month,
                Quarter = result.Quarter,
                IsActive = result.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
