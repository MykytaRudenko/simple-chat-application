using Data.Repositories;

namespace Data.UnitOfWork;

public interface IUnitOfWork
{
    IChatRepository Chats { get; }
    IUserRepository Users { get; }
    IMessageRepository Messages { get; }
    Task<int> CompleteAsync();
}