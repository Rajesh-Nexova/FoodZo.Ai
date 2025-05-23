using System.Collections.Generic;
using System.Threading.Tasks;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories;

namespace FoodZOAI.UserManagement.Services
{
    public interface IReminderUserService
    {
        Task<IEnumerable<ReminderUser>> GetAllAsync();
        Task<ReminderUser?> GetByIdAsync(int reminderId, int userId);
        Task<ReminderUser> CreateAsync(ReminderUser reminderUser);
        Task<bool> DeleteAsync(int reminderId, int userId);
    }

    public class ReminderUserService : IReminderUserService
    {
        private readonly IReminderUserRepository _repository;

        public ReminderUserService(IReminderUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ReminderUser>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ReminderUser?> GetByIdAsync(int reminderId, int userId)
        {
            return await _repository.GetByIdAsync(reminderId, userId);
        }

        public async Task<ReminderUser> CreateAsync(ReminderUser reminderUser)
        {
            return await _repository.AddAsync(reminderUser);
        }

        public async Task<bool> DeleteAsync(int reminderId, int userId)
        {
            var entity = await _repository.GetByIdAsync(reminderId, userId);
            if (entity == null) return false;

            await _repository.DeleteAsync(entity.Id);
            return true;
        }
    }
}
