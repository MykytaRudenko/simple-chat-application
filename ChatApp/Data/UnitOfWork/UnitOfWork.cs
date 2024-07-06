using Data.Models;
using Data.Repositories;

namespace Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;

    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
        Chats = new ChatRepository(_context);
        Users = new UserRepository(_context);
        Messages = new MessageRepository(_context);
    }

    public IChatRepository Chats { get; }
    public IUserRepository Users { get; }
    public IMessageRepository Messages { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}