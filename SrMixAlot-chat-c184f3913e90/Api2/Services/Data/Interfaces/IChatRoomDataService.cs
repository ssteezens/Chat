using Api.Models.Entities;
using System.Collections.Generic;

namespace Api.Services.Data.Interfaces
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
        ///		Add a chat room to the database.
        /// </summary>
        /// <param name="chatRoom"> Chat room to add. </param>
        /// <returns> Chat room added to the database. </returns>
		ChatRoom Add(ChatRoom chatRoom);
	}
}
