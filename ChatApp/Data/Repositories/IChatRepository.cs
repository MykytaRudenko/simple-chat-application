using Data.Models;

namespace Data.Repositories;

public interface IChatRepository : IRepository<Chat>
{
    Task<IEnumerable<Chat>> GetChatsWithMessagesAsync();
}