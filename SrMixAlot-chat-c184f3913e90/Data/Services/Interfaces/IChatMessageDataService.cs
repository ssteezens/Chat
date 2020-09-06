using Data.Entities;
using System.Collections.Generic;

namespace Data.Services.Interfaces
{
    /// <summary>
    ///		Interface for chat message data service.
    /// </summary>
    public interface IChatMessageDataService
	{
		/// <summary>
        ///		Adds a chat message to the database.
        /// </summary>
        /// <param name="message"> Message to add to the database. </param>
        /// <returns></returns>
		ChatMessage Add(ChatMessage message);

        /// <summary>
        ///     Deletes the chat message with the specified id.
        /// </summary>
        /// <param name="id"> Id of the message to delete. </param>
        ChatMessage Delete(int id);

		/// <summary>
        ///		Gets all chat messages from the database.
        /// </summary>
        /// <returns> All chat messages from the database. </returns>
		IEnumerable<ChatMessage> GetAll();
	}
}
