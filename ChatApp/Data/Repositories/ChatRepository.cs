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
        return await _context.Chats.Include(ch => ch.Messages).ThenInclude(m => m.User).FirstOrDefaultAsync(chat => chat.Id == chatId);
    }

    public async Task<Chat> GetByTitle(string title)
    {
        return await _context.Chats.FirstOrDefaultAsync(ch => ch.Title == title);
    }
}