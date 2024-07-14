using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class ApiDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Chat> Chats => Set<Chat>();
    
    public ApiDbContext(DbContextOptions<ApiDbContext> options) 
        : base(options) 
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany<Message>(u => u.Messages)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId);

            modelBuilder.Entity<Chat>()
                .HasIndex(chat => chat.Title)
                .IsUnique();

            modelBuilder.Entity<Chat>()
                .HasOne<User>(chat => chat.CreatedBy)
                .WithMany(u => u.CreatedChats)
                .HasForeignKey(chat => chat.CreatedById);

            modelBuilder.Entity<Chat>()
                .HasMany<Message>(chat => chat.Messages)
                .WithOne(m => m.Chat)
                .HasForeignKey(m => m.ChatId);
    }
}