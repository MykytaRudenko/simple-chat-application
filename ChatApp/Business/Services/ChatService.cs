using Data.Models;
using Data.UnitOfWork;

namespace Business.Services;

public class ChatService : IChatService
{
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Chat>> GetChatsAsync()
    {
        return await _unitOfWork.Chats.GetChatsWithMessagesAsync();
    }

    public async Task<Chat> GetChatByIdAsync(Guid id)
    {
        return await _unitOfWork.Chats.GetByIdAsync(id);
    }

    public async Task<Chat> CreateChatAsync(Chat chat)
    {
        await _unitOfWork.Chats.AddAsync(chat);
        await _unitOfWork.CompleteAsync();
        return chat;
    }

    public async Task DeleteChatAsync(Guid id)
    {
        var chat = await _unitOfWork.Chats.GetByIdAsync(id);
        if (chat != null)
        {
            _unitOfWork.Chats.RemoveAsync(chat);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task AddMessageAsync(Guid chatId, Message message)
    {
        var chat = await _unitOfWork.Chats.GetByIdAsync(chatId);
        if (chat != null)
        {
            ;
            await _unitOfWork.CompleteAsync();
        }
    }
}