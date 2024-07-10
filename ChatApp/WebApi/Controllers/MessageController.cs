using Business.DTOs;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    

    [HttpPost]
    public async Task<ActionResult<Message>> AddMessageToChat([FromBody] AddMessageDto message)
    {
        await _messageService.AddMessageToChatAsync(message);
        return Ok();
    }
}