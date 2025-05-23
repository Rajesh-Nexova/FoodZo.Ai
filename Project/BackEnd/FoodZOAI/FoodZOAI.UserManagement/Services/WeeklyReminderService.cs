using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Configuration.Contracts;

namespace FoodZOAI.UserManagement.Services
{
    public class WeeklyReminderService : IWeeklyReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IWeeklyReminderMapper _mapper;

        public WeeklyReminderService(
            IReminderRepository reminderRepository,
            IWeeklyReminderMapper mapper)
        {
            _reminderRepository = reminderRepository;
            _mapper = mapper;
        }

        public async Task<List<WeeklyReminderDTO>> GetAllWeeklyRemindersAsync()
        {
            var allReminders = await _reminderRepository.GetAllAsync();

            var weeklyReminders = allReminders
                .Where(r => r.Frequency?.ToLower() == "weekly" && r.IsActive)
                .Select(r => (WeeklyReminderDTO)_mapper.MapToDTO(r)) // Explicit cast
                .ToList();

            return weeklyReminders;
        }
    }
}
