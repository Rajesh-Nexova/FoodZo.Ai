using FoodZOAI.UserManagement.Models;

public interface IUserRepository
{
    Task<int> GetActiveUserCountAsync();

    Task<int> GetInactiveUserCountAsync();

    Task<int> GetTotalUserCountAsync();

    Task<List<User>> GetOnlineUsersAsync();

}
