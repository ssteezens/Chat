using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;
		
		/// <summary>
        ///		Constructs a chat room view model.
        /// </summary>
        /// <param name="id"> Id of the chat room. </param>
        /// <param name="chatMessageDataService"> Service for managing chat message data. </param>
		public ChatRoomViewModel(int id, IChatMessageDataService chatMessageDataService)
		{
			_chatMessageDataService = chatMessageDataService;

			Id = id;
			SendMessageCommand = new RelayCommand(SendMessage, () => !string.IsNullOrEmpty(UserText));
			DeleteMessageCommand = new RelayCommand<ChatMessage>(DeleteMessage);
            
			MessengerInstance.Register<NotificationMessage<ChatMessage>>(this, action => HandleChatMessageNotification(action.Content, action.Notification));
		}

        #region Messenger Handlers

		/// <summary>
        ///		Handler for ChatMessage related messages.
        /// </summary>
        /// <param name="chatMessage"> Message to handle. </param>
        /// <param name="notification"> Type of operation. </param>
		private void HandleChatMessageNotification(ChatMessage chatMessage, string notification)
		{
			// todo: set listeners to be specific to chat room
            if (chatMessage.ChatRoomId != Id)
                return;

			switch (notification)
			{
				case "Add":
				{
					// todo: add this to dto model 
					// set user for message
					chatMessage.User = Users.SingleOrDefault(i => i.Id == chatMessage.UserId);

					DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
						if(ChatMessages.All(i => i.Id != chatMessage.Id))
                            ChatMessages.Add(chatMessage);
                    });
                    break;
				}
				case "Remove":
				{
					var messageToRemove = ChatMessages.SingleOrDefault(i => i.Id == chatMessage.Id);

					if (messageToRemove != null)
						DispatcherHelper.CheckBeginInvokeOnUI(() => { ChatMessages.Remove(messageToRemove); }); 

                    break;
				}
				case "Edit":
				{
					var messageToEdit = ChatMessages.SingleOrDefault(i => i.Id == chatMessage.Id);

					if (messageToEdit != null)
						DispatcherHelper.CheckBeginInvokeOnUI(() => messageToEdit.Message = chatMessage.Message);
					
                    break;
				}
			}
		}

        #endregion

        #region Commands 

		/// <summary>
        ///		Gets a command for sending a message.
        /// </summary>
        public RelayCommand SendMessageCommand { get; }

		/// <summary>
        ///		Gets a command for deleting a message.
        /// </summary>
        public RelayCommand<ChatMessage> DeleteMessageCommand { get; }
		
		/// <summary>
		///		Sends the user's message to the server.
		/// </summary>
		private void SendMessage()
		{
			// create chat entry
			var message = new ChatMessage()
			{
				User = UserInstance.Current,
				Message = UserText,
				ChatRoomId = Id
			};

			// submit chat to server
			var addedMessage = _chatMessageDataService.Add(message);

			// add returned message from server which contains id
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
			var confirmationResult = MessageBox.Show("Delete this message?", "Confirmation", MessageBoxButton.YesNo);

			if (confirmationResult == MessageBoxResult.Yes)
			{
				_chatMessageDataService.Delete(message.Id);

				ChatMessages.Remove(ChatMessages.Single(i => i.Id == message.Id));
            }
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
