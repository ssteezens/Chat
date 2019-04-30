using System.Collections.Generic;
using Chat.Models;
using Chat.Services.Data.Interfaces;

namespace Chat.Services.Data
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
