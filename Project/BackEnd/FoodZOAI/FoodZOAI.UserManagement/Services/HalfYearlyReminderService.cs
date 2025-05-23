using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IHalfYearlyReminderService
    {
        Task<IEnumerable<HalfYearlyReminder>> GetAllActiveAsync();
        Task<HalfYearlyReminder?> GetByIdAsync(int id);
        Task<HalfYearlyReminder> CreateAsync(HalfYearlyReminder halfYearlyReminder);
        Task<HalfYearlyReminder?> UpdateAsync(int id, HalfYearlyReminder halfYearlyReminder);
        Task<bool> DeleteAsync(int id);
    }

    public class HalfYearlyReminderService : IHalfYearlyReminderService
    {
        private readonly IHalfYearlyReminderRepository _repository;

        public HalfYearlyReminderService(IHalfYearlyReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HalfYearlyReminder>> GetAllActiveAsync()
        {
            var reminders = await _repository.GetAllAsync();
            return reminders.Where(r => r.IsActive);
        }

        public async Task<HalfYearlyReminder?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<HalfYearlyReminder> CreateAsync(HalfYearlyReminder halfYearlyReminder)
        {
            halfYearlyReminder.IsActive = true;
            halfYearlyReminder.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(halfYearlyReminder); // await the void method
            return halfYearlyReminder; // return the input object after saving
        }

        public async Task<HalfYearlyReminder?> UpdateAsync(int id, HalfYearlyReminder halfYearlyReminder)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Day = halfYearlyReminder.Day;
            existing.Month = halfYearlyReminder.Month;
            existing.Quarter = halfYearlyReminder.Quarter;
            existing.IsActive = halfYearlyReminder.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing); // Note: no return here
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch
            {
                // Log error if needed
                return false;
            }
        }
    }
}
