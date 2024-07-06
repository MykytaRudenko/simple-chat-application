using Data.Models;

namespace Business.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> CreateUserAsync(User user);
}