using Data.Models;

namespace Data.Repositories;

public interface IChatRepository : IRepository<Chat>
{
    Task<Chat> GetChatWithMessagesAsync(Guid chatId);
}