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
					UserName = "sstevens"
				};

				var result = await _userManager.CreateAsync(myUser, "testing123");

				if (result != IdentityResult.Success)
				{
					throw new InvalidOperationException("Could not correct new user");
				}
			}

			var seedUser = new User()
			{
				NickName = "Db Seeded"
			};
			var chatMessage = new ChatMessage()
			{
				Message = "Seeded chat message",
				User = seedUser,
			};
			var chatRoom = new ChatRoom()
			{
				ChatMessages = new List<ChatMessage>() { chatMessage },
				DisplayName = "Seeded Chat Room",
				Users = new List<User>() { seedUser }
			};

			chatMessage.ChatRoom = chatRoom;

            // create sample user data if none exist
            if (!_context.ChatUsers.Any())
            {
                _context.Add(seedUser);
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
