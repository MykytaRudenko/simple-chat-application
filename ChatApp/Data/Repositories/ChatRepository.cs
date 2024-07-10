using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository 
{
    public ChatRepository(ApiDbContext context) 
        : base(context)
    {
    }

    public async Task<Chat> GetChatWithMessagesAsync(Guid chatId)
    {
        return await _context.Chats.Include(c => c.Messages).ThenInclude(m => m.User).FirstOrDefaultAsync(chat => chat.Id == chatId);
    }
}