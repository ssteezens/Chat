using AutoMapper;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.UI.Interfaces;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///		Main view model for application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;
        private readonly INavigationService _navigationService;

		public MainViewModel(IChatRoomDataService chatRoomDataService, INavigationService navigationService)
		{
			_chatRoomDataService = chatRoomDataService;
			_navigationService = navigationService;

            ToggleAddChatRoomControlVisibilityCommand = new RelayCommand(ToggleAddChatRoomVisibility, true);
            
            MessengerInstance.Register<NotificationMessage<string>>(this, action => HandleLoginMessage(action.Notification));
			MessengerInstance.Register<NotificationMessage<ChatRoom>>(this, action => HandleChatRoomMessage(action.Content, action.Notification));
		}

        #region Properties

        private ChatRoomViewModel _selectedChatRoom;

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

        #endregion

        #region Messenger Handlers

        /// <summary>
        ///     Handle login related messages.
        /// </summary>
        /// <param name="message"> The login message. </param>
		private void HandleLoginMessage(string message)
		{
			switch (message)
			{
                case "LoginSuccessful":
				{
					// get available chat rooms
					GetAvailableChatRooms();
					// navigate to main page
					_navigationService.NavigateToUri("/Views/MainPage.xaml");
                    break;
				}
			}
		}
        
        /// <summary>
        ///     Handle chat room related messages.
        /// </summary>
        /// <param name="room"> The chat room. </param>
        /// <param name="message"> The chat room message. </param>
        private void HandleChatRoomMessage(ChatRoom room, string message)
        {
            switch (message)
            {
                case "Add":
                {
                    // send chat room to api
                    room = _chatRoomDataService.AddChatRoom(room);
                    // map to view model
                    var viewmodel = new ChatRoomViewModel(SimpleIoc.Default.GetInstance<IChatMessageDataService>());
                    Mapper.Map(room, viewmodel);
                    // add to available chat rooms
                    AvailableChatRooms.Add(viewmodel);

                    break;
                }
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Toggles add chat room visibility.
        /// </summary>
        public RelayCommand ToggleAddChatRoomControlVisibilityCommand { get; }

        /// <summary>
        ///     Toggles the AddChatRoom visibility.
        /// </summary>
        private void ToggleAddChatRoomVisibility()
        {
            AddChatRoomViewModel.IsOpen = !AddChatRoomViewModel.IsOpen;
        }

        #endregion

        /// <summary>
        ///		Gets all available chat rooms from the data service.
        /// </summary>
        public void GetAvailableChatRooms()
		{
			// get chat room models from data service
			var chatRooms = _chatRoomDataService.GetChatRooms();

			foreach (var room in chatRooms)
			{
				// create ChatRoomViewModel
				var viewmodel = new ChatRoomViewModel(SimpleIoc.Default.GetInstance<IChatMessageDataService>());
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
    }
}
