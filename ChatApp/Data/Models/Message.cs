using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models;

public class Message
{
    [Key]
    public Guid Id { get; set; }
        
    [Required]
    public string Text { get; set; } = string.Empty;
        
    [Required]
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }
        
    [Required]
    public Guid ChatId { get; set; }
    
    [JsonIgnore]
    [ForeignKey(nameof(ChatId))]
    public virtual Chat? Chat { get; set; }

    public DateTime CreatedAt { get; set; }
}