using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Models
{
	/// <summary>
    ///		Seeds chat data.
    /// </summary>
    public class ChatSeeder
    {
        private readonly ChatContext _context;

        public ChatSeeder(ChatContext context)
        {
            _context = context;
        }

		/// <summary>
        ///		Seed data.
        /// </summary>
        public void Seed()
        {
            _context.Database.EnsureCreated();

			var user = new User()
			{
				NickName = "Db Seeded"
			};
			var chatMessage = new ChatMessage()
			{
				Message = "Seeded chat message",
				User = user,
			};
			var chatRoom = new ChatRoom()
			{
				ChatMessages = new List<ChatMessage>() { chatMessage },
				DisplayName = "Seeded Chat Room",
				Users = new List<User>() { user }
			};

			chatMessage.ChatRoom = chatRoom;

            // create sample user data if none exist
            if (!_context.ChatUsers.Any())
            {
                _context.Add(user);
            }

			// create sample chat message data if none exist
			if (!_context.ChatMessages.Any())
			{
				_context.Add(chatMessage);
			}

            // create sample chat room data if none exist
            if (!_context.ChatRooms.Any())
			{
				_context.Add(chatRoom);
			}

            _context.SaveChanges();
        }
    }
}
