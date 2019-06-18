using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;

		public ChatRoomViewModel(IChatMessageDataService chatMessageDataService)
		{
			_chatMessageDataService = chatMessageDataService;

			SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
            DeleteMessageCommand = new RelayCommand<ChatMessage>(DeleteMessage, true);
		}

        #region Event Handlers 

		private bool CanSendMessage => !string.IsNullOrEmpty(UserText);
		
        public RelayCommand SendMessageCommand { get; }
        public RelayCommand<ChatMessage> DeleteMessageCommand { get; }
		
		/// <summary>
		///		Sends the user's message to the server.
		/// </summary>
		private void SendMessage()
		{
			// create chat entry
			var message = new ChatMessage()
			{
				User = ActiveUser,
				Message = UserText,
				ChatRoomId = Id
			};

			// submit chat to server
			var addedMessage = _chatMessageDataService.Add(message);

			// add message to list of messages
			ChatMessages.Add(addedMessage);

			// clear user text
			UserText = string.Empty;
		}

        /// <summary>
        ///     Deletes a chat message.
        /// </summary>
        /// <param name="message"> Message to delete. </param>
        private void DeleteMessage(ChatMessage message)
        {
            _chatMessageDataService.Delete(message.Id);

            ChatMessages.Remove(ChatMessages.Single(i => i.Id == message.Id));
        }

        #endregion

        #region Properties 

        private string _displayName;
		private string _userText;
        private ChatMessage _selectedChatMessage;

		/// <summary>
        ///		Chat room's display name.
        /// </summary>
		public string DisplayName
		{
			get => _displayName;
			set => Set(ref _displayName, value, nameof(DisplayName));
		}

		/// <summary>
		///     Text entered into the user's TextBox.
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
		public ObservableCollection<ChatMessage> ChatMessages { get; set; }

		/// <summary>
		///     All user's in this chat.
		/// </summary>
		public ObservableCollection<User> Users { get; set; }

        /// <summary>
        ///     The selected chat message.
        /// </summary>
        public ChatMessage SelectedChatMessage
        {
            get => _selectedChatMessage;
            set
            {
                if(_selectedChatMessage != null)
                    _selectedChatMessage.IsSelected = false;

                Set(ref _selectedChatMessage, value, nameof(SelectedChatMessage));

                if(SelectedChatMessage != null)
                    SelectedChatMessage.IsSelected = true;
            }
        }

		/// <summary>
		///		Id of the chat room.
		/// </summary>
		public int Id { get; set; }

        #endregion
    }
}
