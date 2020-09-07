using System.Collections.Generic;
using ChatWpf.Models;
using Shared.Models.Models;

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

        /// <summary>
        ///     Add a user to a room.
        /// </summary>
        /// <param name="userModel"> The <see cref="UserModel"/> to add. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        void AddUser(UserModel userModel, int chatRoomId);
    }
}
