using System.Collections.Generic;
using Chat.Models;
using ServiceStack;

namespace Chat.Services.Interfaces
{
    /// <summary>
    ///     Data service used for making api requests.
    /// </summary>
    public interface IDataService
	{
		/// <summary>
		///		Add a chat entry to a chat room.
		/// </summary>
		/// <returns> True if successful. </returns>
		bool AddChatMessage(ChatMessage message);

        /// <summary>
        ///     Return a list of chat entries.
        /// </summary>
        /// <returns> List of chat entries. </returns>
        IEnumerable<ChatMessage> GetChatEntries();

		/// <summary>
		///     Get an enumerable of chat rooms.
		/// </summary>
		/// <returns> Enumerable of chat rooms. </returns>
        IEnumerable<ChatRoom> GetChatRooms();

        /// <summary>
        ///     The client used for api requests.
        /// </summary>
        JsonServiceClient Client { get; set; }
    }
}
