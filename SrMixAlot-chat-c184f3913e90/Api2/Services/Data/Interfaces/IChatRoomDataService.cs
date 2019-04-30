using Api.Models.Entities;
using System.Collections.Generic;

namespace Api.Services.Data.Interfaces
{
    /// <summary>
    ///		Interface for chat room related data operations.
    /// </summary>
    public interface IChatRoomDataService
	{
		/// <summary>
        ///		Gets all chat rooms from the database.
        /// </summary>
        /// <returns> All chat rooms from the database.</returns>
		IEnumerable<ChatRoom> GetAll();
	}
}
