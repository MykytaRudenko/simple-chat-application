namespace Business.DTOs;

public class AddMessageDto
{
    public string Text { get; set; }
    
    public Guid ChatId { get; set; }
    
    public Guid UserId { get; set; }
}