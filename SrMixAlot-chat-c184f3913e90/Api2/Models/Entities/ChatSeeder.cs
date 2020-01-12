using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Api.Models.Entities
{
    /// <summary>
    ///		Seeds chat data.
    /// </summary>
    public class ChatSeeder
    {
        private readonly ChatContext _context;
        private readonly UserManager<User> _userManager;

        public ChatSeeder(ChatContext context, UserManager<User> userManager)
        {
            _context = context;
			_userManager = userManager;
		}

		/// <summary>
        ///		Seed data.
        /// </summary>
        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            var myUser = await _userManager.FindByEmailAsync("sstevens@daytonfreight.com");
			if (myUser == null)
			{
				myUser = new User()
				{
					NickName = "Sam",
					Email = "sstevens@daytonfreight.com",
					UserName = "sstevens",
                    ProfileImageData = "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"
                };

				var result = await _userManager.CreateAsync(myUser, "testing123");

				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Could not correct new user");
				}
			}
            
			var chatMessage = new ChatMessage()
			{
				Message = "Seeded chat message",
				User = myUser,
			};
			var chatRoom = new ChatRoom()
			{
				ChatMessages = new List<ChatMessage>() { chatMessage },
				DisplayName = "Seeded Chat Room",
				Users = new List<User>() { myUser }
			};

			chatMessage.ChatRoom = chatRoom;

            // create sample user data if none exist
            if (!_context.ChatUsers.Any())
            {
                _context.Add(myUser);
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

        private void RemoveUsers()
        {
            var users = _context.Users.ToList();
            var messages = _context.ChatMessages.ToList();
            var rooms = _context.ChatRooms.ToList();

            foreach (var message in messages)
                _context.ChatMessages.Remove(message);
            foreach (var room in rooms)
                _context.ChatRooms.Remove(room);
            foreach (var user in users)
                _context.Users.Remove(user);

            _context.SaveChanges();
        }
    }
}
