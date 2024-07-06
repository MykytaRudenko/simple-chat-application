using Data.Models;

namespace Data.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(ApiDbContext context) 
        : base(context)
    {
    }
}