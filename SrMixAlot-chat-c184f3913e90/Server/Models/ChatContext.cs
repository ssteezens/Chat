using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base (options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<ChatEntry> ChatEntrys { get; set; }
    }
}