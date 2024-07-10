using Business.DTOs;
using Data.Models;

namespace Business.Services;

public interface IMessageService
{
    Task AddMessageToChatAsync(AddMessageDto messageDto);
}