using Data.Models;
using Data.UnitOfWork;

namespace Business.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddMessageToChatAsync(Message message)
    {
        var chat = await _unitOfWork.Chats.GetByIdAsync(message.ChatId);
        if (chat != null)
        {
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.CompleteAsync();
        }
    }
}