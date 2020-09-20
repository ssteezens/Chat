namespace Shared.Models.Models
{
    /// <summary>
    ///		Model for a chat message. 
    /// </summary>
    public class ChatMessageModel
    {
		/// <summary>
        ///		Gets or sets the message's id.
        /// </summary>
		public int Id { get; set; }

		/// <summary>
        ///		Gets or sets the message's room id.
        /// </summary>
		public int ChatRoomId { get; set; }

		/// <summary>
        ///		Gets or sets the message's user id.
        /// </summary>
		public string UserId { get; set; }

		/// <summary>
        ///		Gets or sets the message.
        /// </summary>
		public string Message { get; set; }
    }
}
