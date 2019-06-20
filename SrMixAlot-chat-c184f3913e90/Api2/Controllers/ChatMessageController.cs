using Api.Models.Entities;
using Api.Services.Connection.Interfaces;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat message related actions.
    /// </summary>
    public class ChatMessageController : Controller
    {
        private readonly IChatMessageDataService _chatMessageDataService;
		private readonly IMessageService _messageService;

		public ChatMessageController(IChatMessageDataService chatMessageDataService, IMessageService messageService)
		{
			_chatMessageDataService = chatMessageDataService;
			_messageService = messageService;
		}

		/// <summary>
        ///		Adds a chat message to the database.
        /// </summary>
        /// <param name="message"> Message to add to the database. </param>
        /// <returns> The message added to the database. </returns>
        [HttpPost("/ChatMessage/Add")]
        public IActionResult Add([FromBody] ChatMessage message)
		{
			_messageService.SendMessageToExchange($"Chat.Room.{message.ChatRoomId}", message.Message);

			return Ok(_chatMessageDataService.Add(message));
		}

        /// <summary>
        ///     Deletes a chat message. 
        /// </summary>
        /// <param name="id"> Id of the messaage to delete. </param>
        /// <returns> An ok. </returns>
        [HttpGet("/ChatMessage/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _chatMessageDataService.Delete(id);

            return Ok();
        }

		/// <summary>
        ///		Gets all chat messages from chat context.
        /// </summary>
        /// <returns> All messages from chat context. </returns>
        [HttpGet("/ChatMessage/GetAll")]
        public IActionResult GetAll()
		{
			return Ok(_chatMessageDataService.GetAll());
		}
    }
}