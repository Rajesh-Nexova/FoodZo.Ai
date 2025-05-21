using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodZOAI.UserManagement.Services
{
    public interface IFileService
    {
        Task<string> SaveUserProfilePhotoAsync(int userId, IFormFile photo);
    }
}
