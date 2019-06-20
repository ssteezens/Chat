using Api.Models.Dto;
using Api.Models.Entities;
using Api.Services.Connection.Interfaces;
using Api.Services.Data.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat room related actions.
    /// </summary>
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;
		private readonly IMapper _mapper;
		private readonly IExchangeService _exchangeService;

		///  <summary>
		/// 		Chat room controller constructor.
		///  </summary>
		///  <param name="chatRoomDataService"> Injected data service for chat rooms. </param>
		///  <param name="mapper"> Injected AutoMapper. </param>
		///  <param name="exchangeService"> Service for managing connection queue's. </param>
		public ChatRoomController(IChatRoomDataService chatRoomDataService, IMapper mapper, IExchangeService exchangeService)
		{
			_chatRoomDataService = chatRoomDataService;
			_mapper = mapper;
			_exchangeService = exchangeService;
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

            // create chat room
			var room = _chatRoomDataService.Add(chatRoom);

            // create rabbitmq queue for room
            _exchangeService.CreateExchange($"Chat.Room.{room.Id}");
			
			return Ok(room);
		}
    }
}
