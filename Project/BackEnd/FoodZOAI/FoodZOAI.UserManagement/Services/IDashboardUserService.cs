using FoodZOAI.UserManagement.Models;

public interface IDashboardUserService 
{
    Task<int> GetActiveUserCountAsync();

    Task<int> GetInactiveUserCountAsync();

    Task<int> GetTotalUserCountAsync();

    Task<List<User>> GetOnlineUsersAsync();
}
