using Data.Models;

namespace Business.Services;

public interface IMessageService
{
    Task AddMessageToChatAsync(Message message);
}