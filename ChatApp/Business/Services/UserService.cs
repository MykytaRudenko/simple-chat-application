using Business.DTOs;
using Business.Validators;
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

    public async Task<User> CreateUserAsync(CreateUserDto createUserDto)
    {
        var validator = new CreateUserValidator(_unitOfWork);
        var validationResults = validator.Validate(createUserDto);

        if (!validationResults.IsValid)
        {
            return null;
        }
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Login = createUserDto.Login
        };
        
        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return user;
    }
}