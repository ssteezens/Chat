using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	/// <summary>
    ///		Api controller for chat message related actions.
    /// </summary>
    public class ChatMessageController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatMessageController(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

		/// <summary>
        ///		Adds a chat message to the database.
        /// </summary>
        /// <param name="message"> Message to add to the database. </param>
        /// <returns> The message added to the database. </returns>
        [HttpPost]
        [Route("/ChatMessage/Add")]
        public ChatMessage Add([FromBody] ChatMessage message)
		{
			var addedMessage = _chatContext.Add(message).Entity;

			_chatContext.SaveChanges();

			return addedMessage;
		}        

		/// <summary>
        ///		Gets all chat messages from chat context.
        /// </summary>
        /// <returns> All messages from chat context. </returns>
        [HttpGet]
        [Route("/ChatMessage/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_chatContext.ChatMessages.ToList());
        }
    }
}