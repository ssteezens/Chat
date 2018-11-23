using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Api.Services.Data.Interfaces;

namespace Api.Controllers
{
	/// <summary>
    ///		Api controller for chat room related actions.
    /// </summary>
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;

		/// <summary>
        ///		Chat room controller constructor.
        /// </summary>
        /// <param name="chatRoomDataService"> Injected data service for chat rooms. </param>
        public ChatRoomController(IChatRoomDataService chatRoomDataService)
		{
            _chatRoomDataService = chatRoomDataService;
        }

		/// <summary>
        ///		Gets all chat rooms from the database.
        /// </summary>
        /// <returns> All chat rooms from ChatContext. </returns>
        [Route("/ChatRoom/GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_chatRoomDataService.GetAll());
        }
    }
}
