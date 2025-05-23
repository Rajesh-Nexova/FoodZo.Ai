using FoodZOAI.UserManagement.DTOs;
using FoodZOAI.UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Configuration.Contracts
{
    public interface IReminderService
    {
        Task<IEnumerable<ReminderDTO>> GetAllAsync();
        Task<ReminderDTO?> GetByIdAsync(int id);
    }
}
