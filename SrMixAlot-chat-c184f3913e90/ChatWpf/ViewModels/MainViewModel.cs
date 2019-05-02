using System.Collections.ObjectModel;
using ChatWpf.Services.Data.Interfaces;
using ChatWpf.Services.UI.Interfaces;
using GalaSoft.MvvmLight;
using System.Windows.Controls;

namespace ChatWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IChatRoomDataService _chatRoomDataService;
        private readonly INavigationService _navigationService;

		public MainViewModel(IChatRoomDataService chatRoomDataService, INavigationService navigationService)
		{
			_chatRoomDataService = chatRoomDataService;
			_navigationService = navigationService;
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

        #endregion

        #region Messenger Handlers

		private void HandleLoginMessage(string message)
		{
			switch (message)
			{
                case "LoginSuccessful":
				{

                    break;
				}
			}
		}

        #endregion

		public void GetAvailableChatRooms()
		{
			var thing = _chatRoomDataService.GetChatRooms();
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
