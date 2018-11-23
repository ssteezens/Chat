using Chat.Models;
using Chat.Services.Interfaces;
using System.Collections.Generic;

namespace Chat.Services
{
    /// <summary>
    ///     Service for chat room related data.
    /// </summary>
    public class ChatRoomDataService : DataServiceBase, IChatRoomDataService
    {
		/// <summary>
		///     Get an enumerable of chat rooms.
		/// </summary>
		/// <returns> Enumerable of chat rooms. </returns>
		public IEnumerable<ChatRoom> GetChatRooms()
		{
			return Client.Get<IEnumerable<ChatRoom>>("/ChatRoom/GetAll");
		}
    }
}
