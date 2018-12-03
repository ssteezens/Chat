using System.Collections.Generic;
using Chat.Models;
using Chat.Services.Data.Interfaces;

namespace Chat.Services.Data
{
    /// <summary>
    ///		Service for chat message related data.
    /// </summary>
    public class ChatMessageDataService : DataServiceBase, IChatMessageDataService
    {
		/// <summary>
		///		Add a chat entry to a chat room.
		/// </summary>
		/// <returns> True if successful. </returns>
		public ChatMessage AddChatMessage(ChatMessage message)
		{
			return Client.Post<ChatMessage>("/ChatMessage/Add", message);
		}

		/// <summary>
		///     Get an enumerable of chat entries.
		/// </summary>
		/// <returns> List of chat entries. </returns>
		public IEnumerable<ChatMessage> GetChatEntries()
		{
			return Client.Get<IEnumerable<ChatMessage>>("/ChatMessage/GetAll");
		}
    }
}
