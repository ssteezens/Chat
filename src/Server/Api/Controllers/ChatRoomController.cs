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
        private readonly IMessageService _messageService;

        ///  <summary>
        /// 		Chat room controller constructor.
        ///  </summary>
        ///  <param name="chatRoomDataService"> Injected data service for chat rooms. </param>
        ///  <param name="mapper"> Injected AutoMapper. </param>
        ///  <param name="exchangeService"> Service for managing connection queue's. </param>
        ///  <param name="messageService"> Service for sending rabbitmq messages. </param>
        public ChatRoomController(IChatRoomDataService chatRoomDataService, 
            IMapper mapper, 
            IExchangeService exchangeService,
            IMessageService messageService)
		{
			_chatRoomDataService = chatRoomDataService;
			_mapper = mapper;
			_exchangeService = exchangeService;
            _messageService = messageService;
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
            // create chat room
			var room = _chatRoomDataService.Add(chatRoom);
            
			return Ok(room);
		}

        /// <summary>
        ///     Deletes a chat room.
        /// </summary>
        /// <param name="id"> Id of the chat room to delete. </param>
        /// <returns> Action result indicating success or failure. </returns>
		[HttpGet("/ChatRoom/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var removedRoom = _chatRoomDataService.Delete(id);
            var model = _mapper.Map<ChatRoomModel>(removedRoom);
            var clientMessage = new ClientMessage<ChatRoomModel>(model)
            {
                OperationType = MessageOperationTypes.Remove
            };

            _messageService.SendMessageToExchange("Chat.Room.RoomId", clientMessage, model.Id.ToString());

            return Ok();
        }

		/// <summary>
		///		Add user to chat room.
		/// </summary>
		/// <param name="user"> The <see cref="UserModel"/> to add. </param>
		/// <param name="chatRoomId"> The id of the chat room. </param>
		[HttpPost("/ChatRoom/Users/Add/{chatRoomId}")]
        public IActionResult AddUser([FromBody] UserModel user, int chatRoomId)
        {
			var addedUserRoom = _chatRoomDataService.AddUser(user.Username, chatRoomId);
            var model = _mapper.Map<UserRoomModel>(addedUserRoom);
            var clientMessage = new ClientMessage<UserRoomModel>(model)
            {
                OperationType = MessageOperationTypes.Add
            };

            _messageService.SendMessageToExchange("Chat.Room.RoomId", clientMessage, chatRoomId.ToString());

            return Ok();
        }

        /// <summary>
        ///     Removes a user from the chat room.
        /// </summary>
        /// <param name="user"> The <see cref="User"/> to remove. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        /// <returns></returns>
		[HttpPost("/ChatRoom/Users/Delete/{chatRoomId}")]
        public IActionResult RemoveUser([FromBody] UserModel user, int chatRoomId)
        {
			var removedUserRoom = _chatRoomDataService.RemoveUser(user.Username, chatRoomId);
            var model = _mapper.Map<UserRoomModel>(removedUserRoom);
            var clientMessage = new ClientMessage<UserRoomModel>(model)
            {
                OperationType = MessageOperationTypes.Remove
            };

            _messageService.SendMessageToExchange("Chat.Room.RoomId", clientMessage, chatRoomId.ToString());

            return Ok();
        }
    }
}
