﻿using Api.Services.Connection.Interfaces;
using AutoMapper;
using Data.Entities;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;

namespace Api.Controllers
{
    /// <summary>
    ///		Api controller for chat message related actions.
    /// </summary>
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatMessageController : Controller
    {
        private readonly IChatMessageDataService _chatMessageDataService;
		private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

		public ChatMessageController(IChatMessageDataService chatMessageDataService, IMessageService messageService, IMapper mapper)
		{
			_chatMessageDataService = chatMessageDataService;
			_messageService = messageService;
			_mapper = mapper;
		}

		/// <summary>
        ///		Adds a chat message to the database.
        /// </summary>
        /// <param name="message"> Message to add to the database. </param>
        /// <returns> The message added to the database. </returns>
        [HttpPost("/ChatMessage/Add")]
        public IActionResult Add([FromBody] ChatMessage message)
		{
			var addedMessage = _chatMessageDataService.Add(message);
			var messageDto = _mapper.Map<ChatMessageModel>(addedMessage);

			messageDto.OperationType = MessageOperationTypes.Add;

            _messageService.SendMessageToExchange("Chat.Room.RoomId", messageDto, messageDto.ChatRoomId.ToString());

			return Ok(messageDto);
		}

        /// <summary>
        ///     Update a chat message.
        /// </summary>
        /// <param name="message"> The <see cref="ChatMessage"/> to update. </param>
        /// <returns> The update result. </returns>
        [HttpPost("/ChatMessage/Update")]
        public IActionResult Update([FromBody] ChatMessage message)
        {
            var thing = _chatMessageDataService.Edit(message);
            var messageDto = _mapper.Map<ChatMessageModel>(thing);

            messageDto.OperationType = MessageOperationTypes.Edit;

            _messageService.SendMessageToExchange("Chat.Room.RoomId", messageDto, messageDto.ChatRoomId.ToString());

            return Ok(messageDto);
        }

        /// <summary>
        ///     Deletes a chat message. 
        /// </summary>
        /// <param name="id"> Id of the message to delete. </param>
        /// <returns> An ok. </returns>
        [HttpGet("/ChatMessage/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var deletedMessage = _chatMessageDataService.Delete(id);
			var messageDto = _mapper.Map<ChatMessageModel>(deletedMessage);

			messageDto.OperationType = MessageOperationTypes.Remove;

			_messageService.SendMessageToExchange("Chat.Room.RoomId", messageDto, messageDto.ChatRoomId.ToString());

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