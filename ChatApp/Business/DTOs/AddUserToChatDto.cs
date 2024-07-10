namespace Business.DTOs;

public class AddUserToChatDto
{
    public Guid ChatId { get; set; }
    
    public Guid UserId { get; set; }
}