using Business.DTOs;
using Business.Validators;
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

    public async Task AddMessageToChatAsync(AddMessageDto messageDto)
    {
        var validator = new AddMessageValidator(_unitOfWork);
        var validationResults = validator.Validate(messageDto);

        if (!validationResults.IsValid)
        {
            return;
        }
        
        var message = new Message()
        {
            Id = Guid.NewGuid(),
            Text = messageDto.Text,
            ChatId = messageDto.ChatId,
            UserId = messageDto.UserId,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };
            
        await _unitOfWork.Messages.AddAsync(message);
        await _unitOfWork.CompleteAsync();
    }
}