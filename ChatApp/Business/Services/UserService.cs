using Data.Models;
using Data.UnitOfWork;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _unitOfWork.Users.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _unitOfWork.Users.GetByIdAsync(id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }
}