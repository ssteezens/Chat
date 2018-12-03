using AutoMapper;
using Chat.Services.Data;
using Chat.Services.Data.Interfaces;
using GalaSoft.MvvmLight;
using StructureMap;
using System.Collections.ObjectModel;
using System.Linq;

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
			var container = new Container(config =>
			{
				config.For<IChatRoomDataService>().Singleton().Use<ChatRoomDataService>()
					.Named("ChatRoomDataService");
			});
			
			_chatRoomDataService = container.GetInstance<IChatRoomDataService>();

            var chatRooms = _chatRoomDataService.GetChatRooms();
			var chatRoomViewModels = chatRooms.Select(Mapper.Map<ChatRoomViewModel>).ToList();

			// Test available chat rooms
			AvailableChatRooms = new ObservableCollection<ChatRoomViewModel>(chatRoomViewModels);
			SelectedChatRoom = AvailableChatRooms.FirstOrDefault();
		}

        #region Properties

        private ChatRoomViewModel _selectedChatRoom;
        private ObservableCollection<ChatRoomViewModel> _availableChatRooms = new ObservableCollection<ChatRoomViewModel>();

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

        #endregion
    }
}
