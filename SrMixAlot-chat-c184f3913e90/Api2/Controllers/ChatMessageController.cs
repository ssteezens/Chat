using Api.Models;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat message related actions.
    /// </summary>
    public class ChatMessageController : ControllerBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;

        public ChatMessageController(IChatMessageDataService chatMessageDataService)
        {
			_chatMessageDataService = chatMessageDataService;
        }

		/// <summary>
        ///		Adds a chat message to the database.
        /// </summary>
        /// <param name="message"> Message to add to the database. </param>
        /// <returns> The message added to the database. </returns>
        [HttpPost]
        [Route("/ChatMessage/Add")]
        public IActionResult Add([FromBody] ChatMessage message)
		{
			return Ok(_chatMessageDataService.Add(message));
		}        

		/// <summary>
        ///		Gets all chat messages from chat context.
        /// </summary>
        /// <returns> All messages from chat context. </returns>
        [HttpGet]
        [Route("/ChatMessage/GetAll")]
        public IActionResult GetAll()
		{
			return Ok(_chatMessageDataService.GetAll());
		}
    }
}