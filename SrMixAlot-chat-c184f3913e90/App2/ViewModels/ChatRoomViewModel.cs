using Chat.Models;
using Chat.Services.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace Chat.ViewModels
{
    /// <summary>
    ///     View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private string _displayName;
        private string _userText = string.Empty;
		private readonly IDataService _dataService;

		public ChatRoomViewModel(IDataService dataService)
		{
			_dataService = dataService;
		}

        /// <summary>
        ///     Chat's display name.
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => Set(ref _displayName, value, nameof(DisplayName));
        }

        #region Event handlers

        /// <summary>
        ///     Event handler for when the submit button is clicked for the
        ///     ActiveUser's TextBox.
        /// </summary>
        /// <param name="sender"> Submit button. </param>
        /// <param name="e"> Not used. </param>
        public void SubmitClicked(object sender, RoutedEventArgs e)
        {
            // create chat entry
            var message = new ChatMessage()
            {
                User = ActiveUser,
                Message = UserText,
				ChatRoomId = Id
            };

            // submit chat to server
			var addedMessage = _dataService.AddChatMessage(message);
			
			var vm = new ChatMessageViewModel()
			{
				Message = addedMessage.Message,
				User = addedMessage.User
			};

			// add message to list of messages
			ChatMessages.Add(vm);

			// clear user text
			UserText = string.Empty;
		}

        #endregion

        #region Properties

        /// <summary>
        ///     Text entered into the user's textbox.
        /// </summary>
        public string UserText
        {
            get => _userText;
            set => Set(ref _userText, value, nameof(UserText));
        }

        /// <summary>
        ///     The active user.
        /// </summary>
        public User ActiveUser { get; set; }

        /// <summary>
        ///     All the chat entry's for this chat pane.
        /// </summary>
        public ObservableCollection<ChatMessageViewModel> ChatMessages { get; set; }

        /// <summary>
        ///     All user's in this chat.
        /// </summary>
        public ObservableCollection<User> Users { get; set; }

		/// <summary>
        ///		Id of the chat room.
        /// </summary>
		public int Id { get; set; }

        #endregion
    }
}
