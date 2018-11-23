using Api.Models;
using System.Collections.Generic;

namespace Api.Services.Data.Interfaces
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
        ///		Gets all chat messages from the database.
        /// </summary>
        /// <returns> All chat messages from the database. </returns>
		IEnumerable<ChatMessage> GetAll();
	}
}
