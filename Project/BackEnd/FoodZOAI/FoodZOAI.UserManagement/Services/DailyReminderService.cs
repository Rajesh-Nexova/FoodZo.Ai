using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IDailyReminderService
    {
        Task<IEnumerable<DailyReminder>> GetAllActiveAsync();
        Task<DailyReminder?> GetByIdAsync(int id);
        Task<DailyReminder> CreateAsync(DailyReminder dailyReminder);
        Task<DailyReminder?> UpdateAsync(int id, DailyReminder dailyReminder);
        Task<bool> DeleteAsync(int id);
    }

    public class DailyReminderService : IDailyReminderService
    {
        private readonly IDailyReminderRepository _dailyReminderRepository;

        public DailyReminderService(IDailyReminderRepository dailyReminderRepository)
        {
            _dailyReminderRepository = dailyReminderRepository;
        }

        public async Task<IEnumerable<DailyReminder>> GetAllActiveAsync()
        {
            var reminders = await _dailyReminderRepository.GetAllAsync();
            return reminders.Where(r => r.IsActive);
        }

        public async Task<DailyReminder?> GetByIdAsync(int id)
        {
            return await _dailyReminderRepository.GetByIdAsync(id);
        }

        public async Task<DailyReminder> CreateAsync(DailyReminder dailyReminder)
        {
            dailyReminder.IsActive = true;
            await _dailyReminderRepository.AddAsync(dailyReminder);
            return dailyReminder;
        }

        public async Task<DailyReminder?> UpdateAsync(int id, DailyReminder dailyReminder)
        {
            var existing = await _dailyReminderRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.DayOfWeek = dailyReminder.DayOfWeek;
            existing.IsActive = dailyReminder.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _dailyReminderRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _dailyReminderRepository.DeleteAsync(id);
            return true;
        }
    }
}
