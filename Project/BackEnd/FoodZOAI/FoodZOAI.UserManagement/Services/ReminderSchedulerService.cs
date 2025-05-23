using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IReminderSchedulerService
    {
        Task<IEnumerable<ReminderScheduler>> GetAllActiveAsync();
        Task<IEnumerable<ReminderScheduler>> GetAllAsync();
        Task<ReminderScheduler?> GetByIdAsync(int id);
        Task<ReminderScheduler> CreateAsync(ReminderScheduler scheduler);
        Task<ReminderScheduler?> UpdateAsync(int id, ReminderScheduler scheduler);
        Task<bool> DeleteAsync(int id);
    }

    public class ReminderSchedulerService : IReminderSchedulerService
    {
        private readonly IReminderSchedulerRepository _repository;

        public ReminderSchedulerService(IReminderSchedulerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReminderScheduler>> GetAllActiveAsync()
        {
            var schedulers = await _repository.GetAllAsync();
            return schedulers.Where(s => s.IsActive);
        }

        public async Task<IEnumerable<ReminderScheduler>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ReminderScheduler?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ReminderScheduler> CreateAsync(ReminderScheduler scheduler)
        {
            scheduler.IsActive = true;
            scheduler.CreatedDate = DateTime.UtcNow;
            await _repository.AddAsync(scheduler);
            return scheduler;
        }


        public async Task<ReminderScheduler?> UpdateAsync(int id, ReminderScheduler scheduler)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Duration = scheduler.Duration;
            existing.IsActive = scheduler.IsActive;
            existing.Frequency = scheduler.Frequency;
            existing.IsEmailNotification = scheduler.IsEmailNotification;
            existing.Subject = scheduler.Subject;
            existing.Message = scheduler.Message;
            existing.IsRead = scheduler.IsRead;
            existing.UserId = scheduler.UserId;

            await _repository.UpdateAsync(existing); // returns void / Task

            return existing; // return updated entity manually
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsActive = false;
            await _repository.UpdateAsync(existing);
            return true;
        }
    }
}
