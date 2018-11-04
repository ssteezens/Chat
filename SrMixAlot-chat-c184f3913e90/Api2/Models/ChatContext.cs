using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    /// <summary>
    ///     Main db context.
    /// </summary>
    public class ChatContext : DbContext
    {
        public ChatContext(DbContextOptions<ChatContext> options)
            : base (options)
        { }

        /// <summary>
        ///     Set of users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        ///     Set of chat entries.
        /// </summary>
        public DbSet<ChatEntry> ChatEntrys { get; set; }

		/// <summary>
        ///		Set of chat rooms.
        /// </summary>
		public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}