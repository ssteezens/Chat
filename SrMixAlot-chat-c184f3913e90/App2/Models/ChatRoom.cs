using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Chat.Models
{
    public class ChatRoom : ObservableObject
    {
        /// <summary>
        ///     Chat's display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     All the chat entry's for this chat pane.
        /// </summary>
        public ObservableCollection<ChatEntry> ChatEntries { get; set; }

        /// <summary>
        ///     All user's in this chat.
        /// </summary>
        public ObservableCollection<User> Users { get; set; }
    }
}
