using System.Collections.Generic;

namespace Api.Models.Dto
{
    /// <summary>
    ///		DTO object for a chat room.
    /// </summary>
    public class ChatRoomDto
    {
		/// <summary>
        ///		Gets or sets the room's id.
        /// </summary>
		public int Id { get; set; }

		/// <summary>
        ///		Gets or sets the room's display name.
        /// </summary>
		public string DisplayName { get; set; }

		/// <summary>
        ///		Gets or sets the room's messages.
        /// </summary>
		public IEnumerable<ChatMessageDto> ChatMessages { get; set; }

		/// <summary>
        ///		Gets or sets the room's users.
        /// </summary>
		public IEnumerable<UserDto> Users { get; set; }
    }
}
