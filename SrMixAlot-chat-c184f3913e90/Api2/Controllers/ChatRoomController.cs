using System.Collections.Generic;
using Api.Models.Dto;
using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat room related actions.
    /// </summary>
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;
		private readonly IMapper _mapper;

		///  <summary>
		/// 		Chat room controller constructor.
		///  </summary>
		///  <param name="chatRoomDataService"> Injected data service for chat rooms. </param>
		///  <param name="mapper"> Injected AutoMapper. </param>
		public ChatRoomController(IChatRoomDataService chatRoomDataService, IMapper mapper)
		{
			_chatRoomDataService = chatRoomDataService;
			_mapper = mapper;
		}

		/// <summary>
        ///		Gets all chat rooms from the database.
        /// </summary>
        /// <returns> All chat rooms from ChatContext. </returns>
        [HttpGet("/ChatRoom/GetAll")]
        public IActionResult GetAll()
		{
			var rooms = _chatRoomDataService.GetAll();
			// todo: return dto from service?
			var roomDtos = _mapper.Map<IEnumerable<ChatRoomDto>>(rooms);

            return Ok(roomDtos);
        }

		/// <summary>
        ///		Adds a chat room to the database.
        /// </summary>
        /// <param name="chatRoom"> Chat room to add to the database. </param>
        /// <returns> An Ok or BadRequest. </returns>
		[HttpPost("/ChatRoom/Add")]
		public IActionResult Add([FromBody] ChatRoom chatRoom)
		{
			// todo: validation
			
			return Ok(_chatRoomDataService.Add(chatRoom));
		}
    }
}
