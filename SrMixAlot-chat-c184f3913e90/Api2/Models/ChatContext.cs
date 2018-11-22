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
        ///		Configuring entities.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// TODO: configure one to many relationships here

			// TODO: create user to chat room linking table
		}

        /// <summary>
        ///     Set of users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        ///     Set of chat entries.
        /// </summary>
        public DbSet<ChatMessage> ChatMessages { get; set; }

		/// <summary>
        ///		Set of chat rooms.
        /// </summary>
		public DbSet<ChatRoom> ChatRooms { get; set; }
    }
}