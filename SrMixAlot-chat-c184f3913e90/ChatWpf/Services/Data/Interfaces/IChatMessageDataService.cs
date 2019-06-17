using System.Collections.Generic;
using ChatWpf.Models;

namespace ChatWpf.Services.Data.Interfaces
{
	/// <summary>
    ///		Interface for chat message data service.
    /// </summary>
    public interface IChatMessageDataService : IDataServiceBase
    {
		/// <summary>
		///		Add a chat entry to a chat room.
		/// </summary>
		/// <returns> True if successful. </returns>
		ChatMessage Add(ChatMessage message);

        /// <summary>
        ///     Deletes a chat message.
        /// </summary>
        /// <param name="id"> Id of the message. </param>
        void Delete(int id);

		/// <summary>
		///     Return a list of chat entries.
		/// </summary>
		/// <returns> List of chat entries. </returns>
		IEnumerable<ChatMessage> GetChatEntries();
    }
}
