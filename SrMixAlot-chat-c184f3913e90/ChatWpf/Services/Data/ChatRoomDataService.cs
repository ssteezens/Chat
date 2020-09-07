using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shared.Models.Models;

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
            // todo: take dto model and do this with mapping 
            var rooms = _jsonServiceClient.Get<IEnumerable<ChatRoom>>("/ChatRoom/GetAll");

            foreach (var room in rooms)
                foreach (var message in room.ChatMessages)
                    message.User = room.Users.SingleOrDefault(i => i.Id == message.UserId);
             
            return rooms;
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
            var chatRoomDto = Mapper.Map<ChatRoomModel>(chatRoom);

            return _jsonServiceClient.Post<ChatRoom>("/ChatRoom/Add", chatRoomDto);
		}

        /// <summary>
        ///     Add a user to a room.
        /// </summary>
        /// <param name="userModel"> The <see cref="UserModel"/> to add. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        public void AddUser(UserModel userModel, int chatRoomId)
        {
            _jsonServiceClient.Post<UserModel>($"/ChatRoom/Users/Add/{chatRoomId}", userModel);
        }
    }
}
