using System.Collections.Generic;
using Chat.Models;

namespace Chat.Services.Data.Interfaces
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
		ChatMessage AddChatMessage(ChatMessage message);

		/// <summary>
		///     Return a list of chat entries.
		/// </summary>
		/// <returns> List of chat entries. </returns>
		IEnumerable<ChatMessage> GetChatEntries();
    }
}
