using System.Collections.Generic;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;

namespace ChatWpf.Services.Data
{
    /// <summary>
    ///     Service for chat room related data.
    /// </summary>
    public class ChatRoomDataService : IChatRoomDataService
    {
        private readonly IJsonServiceClient _jsonServiceClient;

		public ChatRoomDataService(IJsonServiceClient jsonServiceClient)
		{
			_jsonServiceClient = jsonServiceClient;
		}

		/// <summary>
		///     Get an enumerable of chat rooms.
		/// </summary>
		/// <returns> Enumerable of chat rooms. </returns>
		public IEnumerable<ChatRoom> GetChatRooms()
		{
            return _jsonServiceClient.Get<IEnumerable<ChatRoom>>("/ChatRoom/GetAll");
		}

		/// <summary>
		///		Get an enumeration of chat rooms available to the user.
		/// </summary>
		/// <returns> Chat rooms available to the user. </returns>
		public IEnumerable<ChatRoom> GetForUser()
		{
			return _jsonServiceClient.Get<IEnumerable<ChatRoom>>("/ChatRoom/GetForUser");
        }

		/// <summary>
		///		Send a chat room to api to add to the database.
		/// </summary>
		/// <returns> ChatRoom added to the database. </returns>
		public ChatRoom AddChatRoom(ChatRoom chatRoom)
		{
			return _jsonServiceClient.Post<ChatRoom>("/ChatRoom/Add", chatRoom);
		}
	}
}
