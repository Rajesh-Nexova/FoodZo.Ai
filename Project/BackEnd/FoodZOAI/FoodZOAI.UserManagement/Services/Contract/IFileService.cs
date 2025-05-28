using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FoodZOAI.UserManagement.Services.Contract
{
    public interface IFileService
    {
        Task<string> SaveUserProfilePhotoAsync(IFormFile photo);
    }
}
