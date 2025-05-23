using FoodZOAI.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodZOAI.UserManagement.Repositories
{
    public class MonthlyReminderRepository : IMonthlyReminderRepository
    {
        private readonly List<MonthlyReminder> _reminders = new()
        {
            new MonthlyReminder
            {
                Id = 1,
                Subject = "Monthly Report",
                Message = "Prepare and send monthly report",
                Frequency = "Monthly",
                DayOfMonth = 1,
                StartDate = DateTime.Now,
                IsRepeated = true,
                IsEmailNotification = true,
                CreatedAt = DateTime.Now,
                IsActive = true,
                CreatedByUser = "admin"
            }
        };

        public Task<IEnumerable<MonthlyReminder>> GetAllAsync()
        {
            return Task.FromResult(_reminders.Where(r => r.IsActive).AsEnumerable());
        }

        public Task<MonthlyReminder?> GetByIdAsync(int id)
        {
            var reminder = _reminders.FirstOrDefault(r => r.Id == id && r.IsActive);
            return Task.FromResult(reminder);
        }
    }
}
