using Api.Services.Connection.Interfaces;
using AutoMapper;
using Data.Entities;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Shared.Models.Models;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat room related actions.
    /// </summary>
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
			var rooms = _chatRoomDataService.GetAll(User.Identity.Name);
			var roomDtos = _mapper.Map<IEnumerable<ChatRoomModel>>(rooms);
			
            return Ok(roomDtos);
        }
		
		/// <summary>
        ///		Adds a chat room to the database.
        /// </summary>
        /// <param name="chatRoom"> Chat room to add to the database. </param>
        /// <returns> An Ok or BadRequest. </returns>
		[HttpPost("/ChatRoom/Add")]
		public IActionResult Add([FromBody] ChatRoomModel chatRoom)
		{
            // todo: validation

            // create chat room
			var room = _chatRoomDataService.Add(chatRoom);

			return Ok(room);
		}

		/// <summary>
		///		Add user to chat room.
		/// </summary>
		/// <param name="user"> The <see cref="UserModel"/> to add. </param>
		/// <param name="chatRoomId"> The id of the chat room. </param>
		[HttpPost("/ChatRoom/Users/Add/{chatRoomId}")]
        public IActionResult AddUser([FromBody] UserModel user, int chatRoomId)
        {
			_chatRoomDataService.AddUser(user.Username, chatRoomId);

            return Ok();
        }
    }
}
