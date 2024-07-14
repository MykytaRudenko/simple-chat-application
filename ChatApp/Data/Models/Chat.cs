﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Data.Models;

public class Chat
{
    [Key]
    public Guid Id { get; set; }

    [Required] 
    public string Title { get; set; } = string.Empty;
        
    [Required]
    public Guid CreatedById { get; set; }
    
    [JsonIgnore]
    [ForeignKey(nameof(CreatedById))]
    public virtual User CreatedBy { get; set; }
        
    public DateTime CreatedAt { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}