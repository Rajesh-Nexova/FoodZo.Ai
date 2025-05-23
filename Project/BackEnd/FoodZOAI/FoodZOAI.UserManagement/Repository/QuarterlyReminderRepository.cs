using FoodZOAI.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class QuarterlyReminderRepository : IQuarterlyReminderRepository
    {
        private readonly List<QuarterlyReminder> _reminders = new()
        {
            new QuarterlyReminder
            {
                Id = 1,
                ReminderId = 101,
                Day = 15,
                Month = 3,
                Quarter = 1,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Reminder = new Reminder { Id = 101 } 
            },
            new QuarterlyReminder
            {
                Id = 2,
                ReminderId = 102,
                Day = 15,
                Month = 6,
                Quarter = 2,
                CreatedAt = DateTime.Now,
                IsActive = true,
                Reminder = new Reminder { Id = 102 }
            }
        };

        public Task<IEnumerable<QuarterlyReminder>> GetAllAsync()
        {
            return Task.FromResult(_reminders.Where(r => r.IsActive).AsEnumerable());
        }

        public Task<QuarterlyReminder?> GetByIdAsync(int id)
        {
            var reminder = _reminders.FirstOrDefault(r => r.Id == id && r.IsActive);
            return Task.FromResult(reminder);
        }
    }
}
