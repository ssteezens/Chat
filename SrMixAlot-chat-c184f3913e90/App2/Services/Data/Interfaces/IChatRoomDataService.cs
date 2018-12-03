using System.Collections.Generic;
using Chat.Models;

namespace Chat.Services.Data.Interfaces
{
	/// <summary>
    ///		Interface for chat room data service.
    /// </summary>
    public interface IChatRoomDataService : IDataServiceBase
    {
		/// <summary>
		///     Get an enumerable of chat rooms.
		/// </summary>
		/// <returns> Enumerable of chat rooms. </returns>
		IEnumerable<ChatRoom> GetChatRooms();
    }
}
