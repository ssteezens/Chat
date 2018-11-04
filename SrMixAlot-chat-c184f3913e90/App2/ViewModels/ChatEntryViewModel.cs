using Chat.Models;
using GalaSoft.MvvmLight;

namespace Chat.ViewModels
{
    public class ChatEntryViewModel : ViewModelBase
    {

        /// <summary>
        ///     The user that sent the chat entry.
        /// </summary>
        public User User { get; set; }

        private string _message;

        /// <summary>
        ///     The text of the chat entry
        /// </summary>
        public string Message
        {
            get => _message;
            set => Set(ref _message, value, nameof(Message));
        }
    }
}
