namespace Business.DTOs;

public class CreateChatDto
{
    public string Title { get; set; }
    
    public Guid CreatedById { get; set; }
}