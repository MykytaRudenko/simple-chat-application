using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;

    public ChatHub(IChatService chatService, IUserService userService, IMessageService messageService)
    {
        _chatService = chatService;
        _userService = userService;
        _messageService = messageService;
    }

    public async Task SendMessage(string chatId, string message, string userId)
    {
        var user = await _userService.GetUserByIdAsync(Guid.Parse(userId));
        var chat = await _chatService.GetChatByIdAsync(Guid.Parse(chatId));
        if (user != null && chat != null)
        {
            var messageDto = new AddMessageDto()
            {
                Text = message,
                ChatId = Guid.Parse(chatId),
                UserId = Guid.Parse(userId)
            };
            
            await _messageService.AddMessageToChatAsync(messageDto);
            await Clients.Group(chatId).SendAsync("ReceiveMessage", userId, message);
        }
    }

    public async Task JoinChat(string chatId)
    {
        try
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("UserJoined", Context.ConnectionId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }

    public async Task LeaveChat(string chatId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        await Clients.Group(chatId).SendAsync("UserLeft", Context.ConnectionId);
    }
}