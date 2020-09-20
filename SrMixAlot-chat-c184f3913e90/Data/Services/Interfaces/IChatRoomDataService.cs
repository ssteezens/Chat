using Data.Entities;
using System.Collections.Generic;
using Shared.Models.Models;

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
        /// <param name="chatRoomModel"> Chat room to add. </param>
        /// <returns> Chat room added to the database. </returns>
        ChatRoomModel Add(ChatRoomModel chatRoomModel);

        /// <summary>
        ///     Deletes a chat room in the database.
        /// </summary>
        /// <param name="id"> Id of the chat room to delete. </param>
        /// <returns> The deleted chat room. </returns>
        ChatRoom Delete(int id); 

        /// <summary>
        ///		Add a user to the chat room.
        /// </summary>
        /// <param name="username"> The username to add. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        UserRoom AddUser(string username, int chatRoomId);

        /// <summary>
        ///     Removes a user to chat room association in the database.
        /// </summary>
        /// <param name="username"> Username of the user to chat room relation. </param>
        /// <param name="chatRoomId"> The id of the chat room. </param>
        /// <returns> The removed user to chat room relation. </returns>
        UserRoom RemoveUser(string username, int chatRoomId);
    }
}
