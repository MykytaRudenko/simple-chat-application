using Business.DTOs;
using Data.Models;

namespace Business.Services;

public interface IChatService
{
    Task<IEnumerable<Chat>> GetChatsAsync();
    Task<Chat> GetChatByIdAsync(Guid id);
    Task<Chat> CreateChatAsync(CreateChatDto chatDto);
    Task DeleteChatAsync(Guid id);
}