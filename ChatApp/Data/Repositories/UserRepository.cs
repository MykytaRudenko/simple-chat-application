using Data.Models;

namespace Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApiDbContext context) 
        : base(context)
    {
    }
    
    public async Task<User> GetByLogin(string login)
    {
        return _context.Users.FirstOrDefault(u => u.Login == login);
    }
}