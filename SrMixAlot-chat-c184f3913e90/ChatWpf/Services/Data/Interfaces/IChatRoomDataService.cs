using System.Collections.Generic;
using ChatWpf.Models;

namespace ChatWpf.Services.Data.Interfaces
{
	/// <summary>
    ///		Interface for chat room data service.
    /// </summary>
    public interface IChatRoomDataService 
    {
		/// <summary>
		///     Get an enumerable of chat rooms.
		/// </summary>
		/// <returns> Enumerable of chat rooms. </returns>
		IEnumerable<ChatRoom> GetChatRooms();

		/// <summary>
        ///		Get an enumeration of chat rooms available to the user.
        /// </summary>
        /// <returns> Chat rooms available to the user. </returns>
		IEnumerable<ChatRoom> GetForUser();

		/// <summary>
        ///		Send a chat room to api to add to the database.
        /// </summary>
        /// <returns> ChatRoom added to the database. </returns>
		ChatRoom AddChatRoom(ChatRoom chatRoom);
	}
}
