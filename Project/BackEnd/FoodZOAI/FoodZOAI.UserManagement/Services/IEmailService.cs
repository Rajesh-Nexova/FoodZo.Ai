using FoodZOAI.UserManagement.DTOs;

namespace FoodZOAI.UserManagement.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(SendEmailDTO emailDto);
    }
}
