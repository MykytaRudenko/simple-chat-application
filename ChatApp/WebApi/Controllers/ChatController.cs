using Business.DTOs;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers;

[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Chat>>> GetChats()
    {
        var chats = await _chatService.GetChatsAsync();
        return Ok(chats);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Chat>> GetChat(Guid id)
    {
        var chat = await _chatService.GetChatByIdAsync(id);
        if (chat == null)
        {
            return NotFound();
        }
        return Ok(chat);
    }

    [HttpPost]
    public async Task<ActionResult<Chat>> CreateChat([FromBody] CreateChatDto chatDto)
    {
        var createdChat = await _chatService.CreateChatAsync(chatDto);

        if (createdChat == null)
        {
            return ValidationProblem();
        }
        
        return CreatedAtAction(nameof(GetChat), new { id = createdChat.Id }, createdChat);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteChat(Guid id)
    {
        await _chatService.DeleteChatAsync(id);
        return NoContent();
    }
}