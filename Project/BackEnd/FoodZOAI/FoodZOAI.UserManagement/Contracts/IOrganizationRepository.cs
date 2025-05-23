using FoodZOAI.UserManagement.Models;

namespace FoodZOAI.UserManagement.Contracts
{
    public interface IOrganizationRepository
    {
        Task<Organization?> GetByIdAsync(int id);
        Task UpdateAsync(Organization organization);
    }
}
