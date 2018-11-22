namespace Chat.Models
{
    public class ChatMessage
	{
		/// <summary>
		///     The chat entry's id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
        ///		The id of the chat room.
        /// </summary>
		public int ChatRoomId { get; set; }

		/// <summary>
		///     The user that sent the chat entry.
		/// </summary>
		public User User { get; set; }

		/// <summary>
		///     The text of the chat entry
		/// </summary>
		public string Message { get; set; }
	}
}
