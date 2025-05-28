using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IAppsettingRepository
    {
        Task<List<Appsetting>> GetAllAsync();
        Task<Appsetting?> GetByIdAsync(int id);
        Task<Appsetting?> GetByKeyAsync(string key);
        Task<Appsetting> AddAsync(Appsetting setting);
        Task<Appsetting> UpdateAsync(int id, Appsetting setting);
        Task<bool> DeleteAsync(int id);
    }
}
