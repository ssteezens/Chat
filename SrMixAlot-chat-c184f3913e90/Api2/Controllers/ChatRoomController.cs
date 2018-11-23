using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    public class ChatRoomController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatRoomController(ChatContext chatContext)
		{
            _chatContext = chatContext;
        }

        [Route("/ChatRoom/GetAll")]
        public IActionResult GetAll()
        {
			var users = _chatContext.Users.ToList();
			var messages = _chatContext.ChatMessages.ToList();
			var chatRooms = _chatContext.ChatRooms.ToList();

			// TODO: configure entities 
			// TODO: get messages by ChatRoom id
			foreach (var room in chatRooms)
			{
				room.Users = users;
				room.ChatEntries = messages.Where(i => i.ChatRoomId == room.Id);
			}

            return Ok(chatRooms);
        }
    }
}
