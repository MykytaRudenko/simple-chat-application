using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required] 
    public string Login { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<Chat> CreatedChats { get; set; } = new List<Chat>();

    [JsonIgnore]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}