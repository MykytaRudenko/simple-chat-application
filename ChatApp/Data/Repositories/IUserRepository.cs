using Data.Models;

namespace Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByLogin(string login);
}