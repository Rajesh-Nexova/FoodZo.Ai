using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IReminderService
    {
        Task<IEnumerable<Reminder>> GetAllActiveAsync();
        Task<IEnumerable<Reminder>> GetAllAsync();
        Task<Reminder?> GetByIdAsync(int id);
        Task<Reminder> CreateAsync(Reminder reminder);
        Task<Reminder?> UpdateAsync(int id, Reminder reminder);
        Task<bool> DeleteAsync(int id);
    }

    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _repository;

        public ReminderService(IReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reminder>> GetAllActiveAsync()
        {
            var reminders = await _repository.GetAllAsync();
            return reminders.Where(r => r.IsActive);
        }

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Reminder?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Reminder> CreateAsync(Reminder reminder)
        {
            reminder.IsActive = true;
            reminder.CreatedAt = DateTime.UtcNow;
            await _repository.AddAsync(reminder); // no return, just await
            return reminder;
        }

        public async Task<Reminder?> UpdateAsync(int id, Reminder reminder)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            // update fields
            existing.Subject = reminder.Subject;
            existing.IsActive = reminder.IsActive;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            // Soft delete
            existing.IsActive = false;
            existing.DeletedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(existing);
            return true;
        }
    }
}
