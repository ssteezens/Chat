using AutoMapper;
using Chat.Models;
using Chat.Services.Data;
using Chat.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;

namespace Chat.ViewModels
{
    /// <summary>
    ///		View model for main entry of the application.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
		private readonly IChatRoomDataService _chatRoomDataService;

        public MainViewModel()
        {	
			_chatRoomDataService = new ChatRoomDataService();

            var chatRooms = _chatRoomDataService.GetChatRooms();
			var chatRoomViewModels = chatRooms.Select(Mapper.Map<ChatRoomViewModel>).ToList();

			// Test available chat rooms
			AvailableChatRooms = new ObservableCollection<ChatRoomViewModel>(chatRoomViewModels);
			SelectedChatRoom = AvailableChatRooms.FirstOrDefault();
		}

        #region Properties

        private User _user;
        private ChatRoomViewModel _selectedChatRoom;
		private AddChatRoomViewModel _addChatRoomViewModel = new AddChatRoomViewModel();
        private ObservableCollection<ChatRoomViewModel> _availableChatRooms = new ObservableCollection<ChatRoomViewModel>();

		/// <summary>
        ///		The current user.
        /// </summary>
		public User User
		{
			get => _user;
			set => Set(ref _user, value, nameof(User));
		}

        /// <summary>
        ///     The selected chat room.
        /// </summary>
        public ChatRoomViewModel SelectedChatRoom
        {
            get => _selectedChatRoom;
            set => Set(ref _selectedChatRoom, value, nameof(SelectedChatRoom));
        }
        
        /// <summary>
        ///     Available chat room view models.
        /// </summary>
        public ObservableCollection<ChatRoomViewModel> AvailableChatRooms
        {
            get => _availableChatRooms;
            private set => Set(ref _availableChatRooms, value, nameof(AvailableChatRooms));
        }

		/// <summary>
        ///		View model for adding a chat room.
        /// </summary>
		public AddChatRoomViewModel AddChatRoomViewModel
		{
            get => _addChatRoomViewModel;
            set => Set(ref _addChatRoomViewModel, value, nameof(AddChatRoomViewModel));
		}

        #endregion

		/// <summary>
        ///		Event handler for when the add chat button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public void ToggleAddChatRoomControl(object sender, RoutedEventArgs e)
		{
            AddChatRoomViewModel.IsOpen = !AddChatRoomViewModel.IsOpen;
		}

		/// <summary>
        ///		Handles navigation from login, sets the current user.
        /// </summary>
        /// <param name="user"></param>
		public void HandleNavigationFromLogin(User user)
		{
            User = user;
		}
    }
}
