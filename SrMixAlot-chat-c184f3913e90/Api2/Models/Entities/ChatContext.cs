using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.Entities
{
    /// <summary>
    ///     Main db context.
    /// </summary>
    public class ChatContext : IdentityDbContext<User>
    {
		public ChatContext(DbContextOptions<ChatContext> options)
			: base(options)
		{
			
		}

		/// <summary>
        ///		Override the on configuring method to enable sensitive data logging when debugging.
        /// </summary>
        /// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
#if DEBUG
            optionsBuilder.EnableSensitiveDataLogging();
#endif
        }

		/// <summary>
        ///		Configuring entities.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			
			ConfigureChatMessage(modelBuilder);
			ConfigureChatRoom(modelBuilder);
		}

		/// <summary>
        ///		Configure the chat user entity.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
		protected virtual void ConfigureChatUser(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(builder =>
			{
				
			});
		}

		/// <summary>
        ///		Configure the chat message entity.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
		protected virtual void ConfigureChatMessage(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ChatMessage>(builder =>
			{
				builder.HasOne(message => message.ChatRoom)
					.WithMany(room => room.ChatMessages);
			});
		}

		/// <summary>
        ///		Configure the chat room entity.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
		protected virtual void ConfigureChatRoom(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ChatRoom>(builder =>
			{
				builder.HasMany(room => room.ChatMessages)
					.WithOne(message => message.ChatRoom);

				builder.HasMany(room => room.Users);
			});
		}

        /// <summary>
        ///     Set of users.
        /// </summary>
        public DbSet<User> ChatUsers { get; set; }

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