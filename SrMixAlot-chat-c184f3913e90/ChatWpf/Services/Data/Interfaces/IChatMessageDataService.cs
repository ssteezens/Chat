using ChatWpf.Models;
using Shared.Models.Models;
using System.Collections.Generic;

namespace ChatWpf.Services.Data.Interfaces
{
    /// <summary>
    ///		Interface for chat message data service.
    /// </summary>
    public interface IChatMessageDataService 
    {
		/// <summary>
		///		Add a chat message to a chat room.
		/// </summary>
		/// <returns> Added message. </returns>
		ChatMessage Add(ChatMessage message);

        /// <summary>
        ///     Edit a chat message.
        /// </summary>
        /// <param name="message"> The <see cref="ChatMessage"/> to edit. </param>
        /// <returns> The edited chat message. </returns>
        ChatMessageModel Edit(ChatMessage message);

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
