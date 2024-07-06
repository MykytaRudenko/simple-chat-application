using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ChatRepository : Repository<Chat>, IChatRepository 
{
    public ChatRepository(ApiDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Chat>> GetChatsWithMessagesAsync()
    {
        return await _context.Chats.Include(c => c.Messages).ToListAsync();
    }
}