using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ChatWpf.Services.MessageBrokering;
using RabbitMQ.Client;

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
			
			// commands
            SendMessageCommand = new RelayCommand(SendMessage, () => !string.IsNullOrEmpty(UserText));
			DeleteMessageCommand = new RelayCommand<ChatMessage>(DeleteMessage);
			ToggleAddUserControlCommand = new RelayCommand(ToggleAddUserControl);

			// view models
            AddUserViewModel = new AddUserViewModel(SimpleIoc.Default.GetInstance<IUserAccountService>(), SimpleIoc.Default.GetInstance<IChatRoomDataService>())
            {
				ChatRoomId = id
            };

            // view model message listeners
			MessengerInstance.Register<NotificationMessage<ChatMessage>>(this, id, action => HandleChatMessageNotification(action.Content, action.Notification));
			MessengerInstance.Register<NotificationMessage<User>>(this, id, action => HandleUserNotification(action.Content, action.Notification));
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

		/// <summary>
		///		Handler for <see cref="User"/> related notifications.
		/// </summary>
		/// <param name="user"> The <see cref="User"/> subject of the notification. </param>
		/// <param name="notification"> The notification that describes the type operation. </param>
        private void HandleUserNotification(User user, string notification)
        {
            switch (notification)
            {
				case "Add":
					DispatcherHelper.CheckBeginInvokeOnUI(() => { Users.Add(user); });
                    break;
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
		///		Command to toggle the add user control open or closed.
		/// </summary>
		public RelayCommand ToggleAddUserControlCommand { get; }
		
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

			// get id from returned message
            message.Id = addedMessage.Id;

			// add returned message from server which contains id
			ChatMessages.Add(message);
			
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

		/// <summary>
		///		Toggle the add user control open or closed.
		/// </summary>
        private void ToggleAddUserControl()
        {
            AddUserViewModel.ControlIsOpen = !AddUserViewModel.ControlIsOpen;
        }

        #endregion

        #region Properties 

        private string _displayName;
		private string _userText;
        private ChatMessage _selectedChatMessage;
        private AddUserViewModel _addUserViewModel;
        private ChatRoom _chatRoomModel;

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

		/// <summary>
		///		View model for adding a user to the room.
		/// </summary>
        public AddUserViewModel AddUserViewModel
        {
            get => _addUserViewModel;
            set => Set(ref _addUserViewModel, value, nameof(AddUserViewModel));
        }

		/// <summary>
		///		The model for the view model.
		/// </summary>
        public ChatRoom ChatRoomModel
        {
            get => _chatRoomModel;
            set => Set(ref _chatRoomModel, value, nameof(ChatRoomModel));
        }

		/// <summary>
		///		Text to display the number of users.
		/// </summary>
        public string UserCountText => $"{Users.Count} Users";

        #endregion
    }
}
