using Chat.Models;
using System.Collections.Generic;

namespace Chat.Services.Interfaces
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
