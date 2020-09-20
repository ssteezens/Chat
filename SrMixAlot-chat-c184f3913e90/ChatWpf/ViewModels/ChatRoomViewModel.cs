using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using AutoMapper;
using Shared.Models.Models;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		View model for a chat room.
    /// </summary>
    public class ChatRoomViewModel : ViewModelBase
    {
        private readonly IChatMessageDataService _chatMessageDataService;
        private readonly IChatRoomDataService _chatRoomDataService;

        ///  <summary>
        /// 		Constructs a chat room view model.
        ///  </summary>
        ///  <param name="id"> Id of the chat room. </param>
        ///  <param name="chatMessageDataService"> Service for managing chat message data. </param>
        ///  <param name="chatRoomDataService"> Service for managing chat room data. </param>
        public ChatRoomViewModel(int id, IChatMessageDataService chatMessageDataService, IChatRoomDataService chatRoomDataService)
		{
			_chatMessageDataService = chatMessageDataService;
            _chatRoomDataService = chatRoomDataService;

            Id = id;
			
			// commands
            SendMessageCommand = new RelayCommand(SendMessage, () => !string.IsNullOrEmpty(UserText));
			DeleteMessageCommand = new RelayCommand<ChatMessage>(DeleteMessage, (messageToDelete) => messageToDelete?.User?.Id == UserInstance.Current?.Id);
			ToggleAddUserControlCommand = new RelayCommand(ToggleAddUserControl);
            DeleteChatRoomCommand = new RelayCommand(DeleteChatRoom);

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
                case "SubmitEdit":
                    _chatMessageDataService.Edit(chatMessage);
                    break;
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
                case "Delete":
                    DispatcherHelper.CheckBeginInvokeOnUI(() => { RemoveUser(user); });
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
        ///     Command to delete a chat room.
        /// </summary>
		public RelayCommand DeleteChatRoomCommand { get; }

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
        
        /// <summary>
        ///     Delete this chat room.
        /// </summary>
        private void DeleteChatRoom()
        {
            MessengerInstance.Send(new NotificationMessage<ChatRoomViewModel>(this, "Remove"));
            _chatRoomDataService.DeleteRoom(Id);
        }

        /// <summary>
        ///     Remove the specified user from the room.
        /// </summary>
        /// <param name="user"> The <see cref="User"/> to remove. </param>
        private void RemoveUser(User user)
        {
            var userDto = Mapper.Map<UserModel>(user);

            _chatRoomDataService.RemoveUser(userDto, Id);

            Users.Remove(user);
        }

        #endregion

        #region Properties 

        private string _displayName;
		private string _userText;
        private ChatMessage _selectedChatMessage;
        private AddUserViewModel _addUserViewModel;
        private ChatRoom _chatRoomModel;
        private bool _userListIsOpen = true;
        private ObservableCollection<User> _users = new ObservableCollection<User>();
        private ObservableCollection<ActionListItem<User>> _userListItems;

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
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users.CollectionChanged -= UserCollectionChangedHandler;

                Set(ref _users, value, nameof(Users));

                Users.CollectionChanged += UserCollectionChangedHandler;
                
                SetActionItems();
            }
        }

        /// <summary>
        ///     Handles the collection changed event on the Users collection.
        /// </summary>
        private void UserCollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetActionItems();
        }

        /// <summary>
        ///     Sets the collection of action list items for the user list.
        /// </summary>
        private void SetActionItems()
        {
            UserListItems = new ObservableCollection<ActionListItem<User>>();

            foreach (var user in Users)
            {
                UserListItems.Add(new ActionListItem<User>(user, MessengerInstance, Id));
            }
        }

        /// <summary>
        ///     List of action items for the user list view.
        /// </summary>
        public ObservableCollection<ActionListItem<User>> UserListItems
        {
            get => _userListItems;
            set => Set(ref _userListItems, value, nameof(UserListItems));
        }

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
        ///     Determines if the user list is open.
        /// </summary>
        public bool UserListIsOpen
        {
            get => _userListIsOpen;
            set => Set(ref _userListIsOpen, value, nameof(UserListIsOpen));
        }

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
