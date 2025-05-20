using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IReminderUserRepository
    {
        Task<List<ReminderUser>> GetAllAsync();
        Task<ReminderUser?> GetByIdAsync(int reminderId ,int id);
        Task<ReminderUser> AddAsync(ReminderUser reminderUser);
        Task<ReminderUser?> UpdateAsync(ReminderUser reminderUser);
        Task<bool> DeleteAsync(int id);
    }
}
