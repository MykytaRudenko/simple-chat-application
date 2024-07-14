using Business.DTOs;
using Business.Validators;
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
        return await _unitOfWork.Chats.GetAllAsync();
    }

    public async Task<Chat> GetChatByIdAsync(Guid id)
    {
        return await _unitOfWork.Chats.GetChatWithMessagesAsync(id);
    }

    public async Task<Chat> CreateChatAsync(CreateChatDto chatDto)
    {
        var validator = new CreateChatValidator(_unitOfWork);
        var validationResults = validator.Validate(chatDto);

        if (!validationResults.IsValid)
        {
            return null;
        }
        
        var chat = new Chat()
        {
            Id = Guid.NewGuid(),
            Title = chatDto.Title,
            CreatedById = chatDto.CreatedById,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };
        chat.CreatedAt = DateTime.Now.ToUniversalTime();
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
}