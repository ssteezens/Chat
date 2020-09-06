using Data.Entities;
using System.Collections.Generic;

namespace Data.Services.Interfaces
{
    /// <summary>
    ///		Interface for chat room related data operations.
    /// </summary>
    public interface IChatRoomDataService
	{
		/// <summary>
        ///		Gets all chat rooms from the database.
        /// </summary>
        /// <returns> All chat rooms from the database.</returns>
		IEnumerable<ChatRoom> GetAll();

		/// <summary>
		///		Gets all chat rooms from the database.
		/// </summary>
		/// <returns> All chat rooms from the database.</returns>
		IEnumerable<ChatRoom> GetAll(string username);

        /// <summary>
        ///		Add a chat room to the database.
        /// </summary>
        /// <param name="chatRoom"> Chat room to add. </param>
        /// <returns> Chat room added to the database. </returns>
        ChatRoom Add(ChatRoom chatRoom);

        /// <summary>
        ///		Add a user to the chat room.
        /// </summary>
        /// <param name="username"> The username to add. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        void AddUser(string username, int chatRoomId);
    }
}
