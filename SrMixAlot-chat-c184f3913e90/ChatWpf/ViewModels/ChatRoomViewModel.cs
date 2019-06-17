using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.MessageRecieving;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Threading;

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

            //MessageConsumer = new MessageConsumer() { Enabled = true, QueueName = DisplayName };
			MessageConsumer = new MessageConsumer() { Enabled = true };

            new Thread(PollMessageUpdates).Start();

			SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);

			MessengerInstance.Register<NotificationMessage<ChatMessage>>(this, action => HandleChatMessageNotification(action.Content, action.Notification));
		}

        #region Messenger Handlers

		private void HandleChatMessageNotification(ChatMessage chatMessage, string notification)
		{
			switch (notification)
			{
				case "Add":
				{
					DispatcherHelper.CheckBeginInvokeOnUI(() => { ChatMessages.Add(chatMessage); });
                    break;
				}
			}
		}

        #endregion

        #region Event Handlers 

        private void PollMessageUpdates()
		{
			// start receiving messages from the server
			MessageConsumer.Start();
        }

		private bool CanSendMessage => !string.IsNullOrEmpty(UserText);
		
        public RelayCommand SendMessageCommand { get; }
		
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
			var addedMessage = _chatMessageDataService.AddChatMessage(message);

			// add message to list of messages
			ChatMessages.Add(addedMessage);

			// clear user text
			UserText = string.Empty;
		}

        #endregion

        #region Properties 

        private string _displayName;
		private string _userText;

		/// <summary>
        ///		Message consumer that keeps chat messages sync'd for a chat room.
        /// </summary>
		public MessageConsumer MessageConsumer { get; set; }

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
		///		Id of the chat room.
		/// </summary>
		public int Id { get; set; }

        #endregion
    }
}
