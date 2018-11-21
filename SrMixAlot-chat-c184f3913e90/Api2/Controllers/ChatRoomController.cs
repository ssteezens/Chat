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
			var entries = _chatContext.ChatEntrys.ToList();
			var chatRooms = _chatContext.ChatRooms.ToList();

			// TODO: configure entities 
			foreach (var room in chatRooms)
			{
				room.Users = users;
				room.ChatEntries = entries;
			}

            return Ok(chatRooms);
        }
    }
}
