using AutoMapper;
using ChatWpf.Models;
using ChatWpf.Services.Connection.Interfaces;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.MessageBrokering;
using ChatWpf.Services.UI.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using CommonServiceLocator;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		Main view model for application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;
        private readonly INavigationService _navigationService;
        private readonly IQueueService _queueService;

		public MainViewModel(IChatRoomDataService chatRoomDataService, INavigationService navigationService, IQueueService queueService)
		{
			_chatRoomDataService = chatRoomDataService;
			_navigationService = navigationService;
			_queueService = queueService;

            ToggleAddChatRoomControlVisibilityCommand = new RelayCommand(ToggleAddChatRoomVisibility, true);
            ShowUserProfileCommand = new RelayCommand(ShowUserProfileControl);
            
            MessengerInstance.Register<NotificationMessage<string>>(this, action => HandleStringMessage(action.Notification));
			MessengerInstance.Register<NotificationMessage<ChatRoom>>(this, action => HandleChatRoomMessage(action.Content, action.Notification));
			
			SetupMessageBrokerListener();
		}

        #region Properties

        private ChatRoomViewModel _selectedChatRoom;
        private bool _userProfileControlIsVisible;

		/// <summary>
        ///		The selected chat room.
        /// </summary>
		public ChatRoomViewModel SelectedChatRoom
		{
			get => _selectedChatRoom;
			set => Set(ref _selectedChatRoom, value, nameof(SelectedChatRoom));
		}

		/// <summary>
        ///		The chat rooms available to the user.
        /// </summary>
		public ObservableCollection<ChatRoomViewModel> AvailableChatRooms { get; set; } = new ObservableCollection<ChatRoomViewModel>();

        /// <summary>
        ///     View model for adding a chat room.
        /// </summary>
        public AddChatRoomViewModel AddChatRoomViewModel { get; set; } = new AddChatRoomViewModel();

		/// <summary>
        ///		Gets or sets the name of the queue for this client.
        /// </summary>
		public string ClientQueueName { get; set; }

		/// <summary>
        ///		Gets or sets the message broker listener.
        /// </summary>
		private MessageBrokerListener MessageBrokerListener { get; set; }

        /// <summary>
        ///     Show or hide the user profile control.
        /// </summary>
        public bool UserProfileControlIsVisible
        {
            get => _userProfileControlIsVisible;
            set => Set(ref _userProfileControlIsVisible, value, nameof(ShowUserProfileControl));
        }

        /// <summary>
        ///     Profile image data in bytes.
        /// </summary>
        public byte[] ProfileImageBytes => Convert.FromBase64String(UserInstance.Current.ProfileImageData);

        #endregion

        #region Messenger Handlers

        /// <summary>
        ///		Handles a login related message.
        /// </summary>
        /// <param name="message"> The message. </param>
        private void HandleStringMessage(string message)
		{
			switch (message)
			{
                case "LoginSuccessful":
				{
					// get available chat rooms
					GetAvailableChatRooms();
                    // default the selected chat room to the first available
                    if (AvailableChatRooms.Any())
                        SelectedChatRoom = AvailableChatRooms.First();
					// navigate to main page
					_navigationService.NavigateToUri("/Views/MainPage.xaml");
                    break;
				}
				case "GoToRegister":
				{
					_navigationService.NavigateToUri("/Views/RegisterPage.xaml");
                    break;
				}
				case "GoBack":
				{
					_navigationService.GoBack();
                    break;
				}
			}
		}
        
		/// <summary>
        ///		Handles a chat room related message.
        /// </summary>
        /// <param name="room"> The chat room. </param>
        /// <param name="message"> The message. </param>
        private void HandleChatRoomMessage(ChatRoom room, string message)
        {
            switch (message)
            {
                case "Add":
                {
                    // send chat room to api
                    room = _chatRoomDataService.AddChatRoom(room);
					// bind queue to chat room's exchange
					_queueService.BindToExchange(ClientQueueName, $"Chat.Room.{room.Id}", string.Empty);
                    // map to view model
                    var viewmodel = new ChatRoomViewModel(room.Id, SimpleIoc.Default.GetInstance<IChatMessageDataService>());
                    Mapper.Map(room, viewmodel);
                    // add to available chat rooms
                    AvailableChatRooms.Add(viewmodel);

                    break;
                }
				case "Remove":
				{
					var roomToRemove = AvailableChatRooms.SingleOrDefault(i => i.Id == room.Id);

					if (roomToRemove != null)
						AvailableChatRooms.Remove(roomToRemove);

					break;
				}
            }
        }
		
        #endregion

        #region Commands

        /// <summary>
        ///     Toggles add chat room visibility.
        /// </summary>
        public RelayCommand ToggleAddChatRoomControlVisibilityCommand { get; }

        public RelayCommand ShowUserProfileCommand { get; }

        /// <summary>
        ///     Toggles the AddChatRoom visibility.
        /// </summary>
        private void ToggleAddChatRoomVisibility()
        {
            AddChatRoomViewModel.IsOpen = !AddChatRoomViewModel.IsOpen;
        }

        private void ShowUserProfileControl()
        {
            var userProfileViewModel = ServiceLocator.Current.GetInstance<UserProfileViewModel>();
            userProfileViewModel.WindowIsVisible = true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///		Sets up the message broker listener.
        /// </summary>
        private void SetupMessageBrokerListener()
		{
			// create unique queue name
			ClientQueueName = Guid.NewGuid().ToString();
			// create a connection queue from this client for this chat room
			_queueService.CreateQueue(ClientQueueName);

            // message broker listener
            MessageBrokerListener = new MessageBrokerListener() { Enabled = true, QueueName = ClientQueueName };

			// start the message broker listener on its own thread
			new Thread(() => MessageBrokerListener.Start()).Start();
		}
		
        /// <summary>
        ///		Gets all available chat rooms from the data service.
        /// </summary>
        public void GetAvailableChatRooms()
		{
			// get chat room models from data service
			var chatRooms = _chatRoomDataService.GetChatRooms();

			foreach (var room in chatRooms)
			{
				// bind queue to chat room's exchange
				_queueService.BindToExchange(ClientQueueName, $"Chat.Room.{room.Id}", string.Empty);
				// create ChatRoomViewModel
				var viewmodel = new ChatRoomViewModel(room.Id, SimpleIoc.Default.GetInstance<IChatMessageDataService>());
				// map chat room into ChatRoomViewModel
				Mapper.Map(room, viewmodel);
				// add to available chat rooms
				AvailableChatRooms.Add(viewmodel);
			}
        }

        /// <summary>
        ///		Sets the navigation service's navigation frame.
        /// </summary>
        /// <param name="frame"></param>
        public void SetNavigationFrame(Frame frame)
		{
			_navigationService.SetNavigationFrame(frame);
		}

		/// <summary>
        ///		Navigate to the login page.
        /// </summary>
		public void NavigateToLogin()
		{
			_navigationService.NavigateToUri("Views/LoginPage.xaml");
		}

        #endregion
    }
}
