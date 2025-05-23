using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodZOAI.UserManagement.Repository
{
    public class OrganizationRepository : IOrganizationRepository
    {

        private readonly FoodZoaiContext _Context;
        public OrganizationRepository(FoodZoaiContext Context)
        {
            _Context = Context;
        }
        public  async Task<Organization?> GetByIdAsync(int id)
        {
            return  await _Context.Organizations.FindAsync(id);
        }

        public async Task UpdateAsync(Organization organization)
        {
             _Context.Organizations.Update(organization);
            await _Context.SaveChangesAsync();

           
        }
    }
}
