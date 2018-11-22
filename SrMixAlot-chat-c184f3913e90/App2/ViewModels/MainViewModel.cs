using System.Collections.Generic;
using Chat.Models;
using Chat.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Linq;

namespace Chat.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
			var dataService = new DataServiceBase();
			var chatRooms = dataService.GetChatRooms();
			var chatRoomViewModels = new List<ChatRoomViewModel>();

			// todo: configure auto mapper profiles for this
			foreach (var room in chatRooms)
			{
				var chatMessageViewModels = new ObservableCollection<ChatMessageViewModel>();

				foreach (var entry in room.ChatEntries)
				{
					var chatMessageViewModel = new ChatMessageViewModel()
					{
						Message = entry.Message,
						User = entry.User
					};

					chatMessageViewModels.Add(chatMessageViewModel);
				}

				var chatRoomViewModel = new ChatRoomViewModel(dataService)
				{
					ChatMessages = chatMessageViewModels,
					DisplayName = room.DisplayName,
					Users = new ObservableCollection<User>(room.Users.ToList()),
				};

				chatRoomViewModels.Add(chatRoomViewModel);
			}
			
            // Test available chat rooms
			AvailableChatRooms = new ObservableCollection<ChatRoomViewModel>(chatRoomViewModels);
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
            set => Set(ref _availableChatRooms, value, nameof(AvailableChatRooms));
        }

        #endregion
    }
}
