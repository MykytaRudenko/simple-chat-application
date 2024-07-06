using Data.Models;

namespace Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApiDbContext context) 
        : base(context)
    {
    }
}