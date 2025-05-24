using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> GetActiveUserCountAsync()
    {
        return await _repository.GetActiveUserCountAsync();
    }

    public async Task<int> GetInactiveUserCountAsync()
    {
        return await _repository.GetInactiveUserCountAsync();

    }

    public async Task<List<User>> GetOnlineUsersAsync()
    {
        return await _repository.GetOnlineUsersAsync();
    }

    public async Task<int> GetTotalUserCountAsync()
    {
        return await _repository.GetTotalUserCountAsync();
    }
}
