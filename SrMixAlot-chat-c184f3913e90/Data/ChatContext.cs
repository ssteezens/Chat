using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
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
            ConfigureChatUserRooms(modelBuilder);
        }

        /// <summary>
        ///		Configure the chat user entity.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
        protected virtual void ConfigureChatUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasMany(user => user.UserRooms)
                    .WithOne(userRoom => userRoom.User)
                    .HasForeignKey(userRoom => userRoom.UserId);
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

                builder.HasOne(message => message.User)
                    .WithMany()
                    .HasForeignKey(message => message.UserId);
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

                builder.HasMany(room => room.UserRooms)
                    .WithOne(userRoom => userRoom.ChatRoom)
                    .HasForeignKey(userRoom => userRoom.ChatRoomId);
            });
        }

        /// <summary>
        ///     Configures the chat user to chat room relational table.
        /// </summary>
        /// <param name="modelBuilder"> The model builder. </param>
        protected virtual void ConfigureChatUserRooms(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoom>(builder =>
            {
                builder.HasKey(key => new { key.ChatRoomId, key.UserId});
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

        /// <summary>
        ///     Relational entity between users and chat rooms.
        /// </summary>
        public DbSet<UserRoom> ChatUserRooms { get; set; }
    }
}