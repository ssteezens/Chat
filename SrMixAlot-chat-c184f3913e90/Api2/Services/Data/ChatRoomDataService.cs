﻿using Api.Models;
using Api.Services.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Api.Services.Data
{
	/// <summary>
    ///		Service for chat room related data.
    /// </summary>
    public class ChatRoomDataService : IChatRoomDataService
	{
		private readonly ChatContext _chatContext;

		/// <summary>
        ///		Chat room constructor.
        /// </summary>
        /// <param name="chatContext"> Ef core chat context. </param>
		public ChatRoomDataService(ChatContext chatContext)
		{
			_chatContext = chatContext;
		}

		/// <summary>
		///		Gets all chat rooms from the database.
		/// </summary>
		/// <returns> All chat rooms from the database.</returns>
		public IEnumerable<ChatRoom> GetAll()
		{
			var users = _chatContext.Users.ToList();
			var messages = _chatContext.ChatMessages.ToList();
			var chatRooms = _chatContext.ChatRooms.ToList();

			// TODO: configure entities 
			foreach (var room in chatRooms)
			{
				room.Users = users;
				room.ChatEntries = messages.Where(i => i.ChatRoomId == room.Id);
			}

			return chatRooms;
		}
	}
}