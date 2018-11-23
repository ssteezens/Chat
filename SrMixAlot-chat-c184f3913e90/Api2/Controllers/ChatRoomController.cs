using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
	/// <summary>
    ///		Api controller for chat room related actions.
    /// </summary>
    public class ChatRoomController : ControllerBase
    {
        private readonly ChatContext _chatContext;

		/// <summary>
        ///		Chat room controller constructor.
        /// </summary>
        /// <param name="chatContext"> Injected ChatContext. </param>
        public ChatRoomController(ChatContext chatContext)
		{
            _chatContext = chatContext;
        }

		/// <summary>
        ///		Gets all chat rooms from the database.
        /// </summary>
        /// <returns> All chat rooms from ChatContext. </returns>
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
