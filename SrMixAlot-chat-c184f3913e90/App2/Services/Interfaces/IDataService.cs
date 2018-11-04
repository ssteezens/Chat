using Chat.Models;
using ServiceStack;
using System.Collections.Generic;

namespace Chat.Services
{
    /// <summary>
    ///     Data service used for making api requests.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        ///     Return a list of chat entries.
        /// </summary>
        /// <returns> List of chat entries. </returns>
        IEnumerable<ChatEntry> GetChatEntries();

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
