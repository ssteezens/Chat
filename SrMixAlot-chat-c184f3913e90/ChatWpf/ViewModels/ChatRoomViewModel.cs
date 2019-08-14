using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.MessageRecieving;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;
		
		public ChatRoomViewModel(int id, string queueName, IChatMessageDataService chatMessageDataService)
		{
            Id = id;

			_chatMessageDataService = chatMessageDataService;

            MessageConsumer = new MessageConsumer() { Enabled = true, QueueName = queueName, ChatRoomId = Id };

            new Thread(PollMessageUpdates).Start();

			SendMessageCommand = new RelayCommand(SendMessage, CanSendMessage);
			DeleteMessageCommand = new RelayCommand<ChatMessage>(DeleteMessage);

			MessengerInstance.Register<NotificationMessage<ChatMessage>>(this, action => HandleChatMessageNotification(action.Content, action.Notification));
		}

        #region Messenger Handlers

		private void HandleChatMessageNotification(ChatMessage chatMessage, string notification)
		{
			switch (notification)
			{
				case "Add":
				{
					// if message is meant for this chat room
					if(chatMessage.ChatRoomId == Id)
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
			_chatMessageDataService.Add(message);
			
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
