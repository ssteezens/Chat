using System.Collections.Generic;

namespace Api.Models
{
    public class ChatRoom
    {
        /// <summary>
        ///     Chat's display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Text entered into the user's textbox.
        /// </summary>
        public string UserText => string.Empty;

        /// <summary>
        ///     The active user.
        /// </summary>
        public User ActiveUser { get; set; }

        /// <summary>
        ///     All the chat entry's for this chat pane.
        /// </summary>
        public IEnumerable<ChatEntry> ChatEntrys { get; set; }

        /// <summary>
        ///     All user's in this chat.
        /// </summary>
        public IEnumerable<User> Users { get; set; }
    }
}
