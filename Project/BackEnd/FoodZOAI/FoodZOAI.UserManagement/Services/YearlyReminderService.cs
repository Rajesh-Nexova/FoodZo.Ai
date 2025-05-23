using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Configuration.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Mappers.Interfaces;

namespace FoodZOAI.UserManagement.Services
{
    public class YearlyReminderService : IYearlyReminderService
    {
        private readonly IYearlyReminderRepository _yearlyReminderRepository;
        private readonly IYearlyReminderMapper _mapper;

        public YearlyReminderService(IYearlyReminderRepository yearlyReminderRepository, IYearlyReminderMapper mapper)
        {
            _yearlyReminderRepository = yearlyReminderRepository;
            _mapper = mapper;
        }

        public async Task<List<YearlyReminderDTO>> GetAllYearlyRemindersAsync()
        {
            var yearlyReminders = await _yearlyReminderRepository.GetAllAsync();

            var dtoList = yearlyReminders
                .Where(r => r.IsActive)
                .Select(r => _mapper.MapToDTO(r))
                .ToList();

            return dtoList;
        }

        public Task<WeeklyReminderDTO?> GetYearlyReminderByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<WeeklyReminderDTO>> GetYearlyRemindersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
