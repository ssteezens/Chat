using System.Collections.Generic;

namespace Api.Models.Entities
{
    public class ChatRoom
    {
		/// <summary>
        ///		Chat room public key.
        /// </summary>
		public int Id { get; set; }

        /// <summary>
        ///     Chat's display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     All the chat messages for this chat pane.
        /// </summary>
        public IEnumerable<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        ///     All user's in this chat.
        /// </summary>
        public IEnumerable<User> Users { get; set; }
    }
}
