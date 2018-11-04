using Chat.Models;
using Chat.Services;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Chat.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {

            var testUser1 = new User()
            {
                NickName = "Test User 1",
                ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
            };
            var testUser2 = new User()
            {
                NickName = "Test User 2",
                ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
            };
            var testUser3 = new User()
            {
                NickName = "Test User 3",
                ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
            };

            var activeUser = new User()
            {
                NickName = "This User",
                ImageFilePath = "ms-appx:///Assets/avatar64x64.png"
            };

            // test user data
            var users = new ObservableCollection<User>()
            {
                testUser1,
                testUser2,
                testUser3
            };

            // test chat entry data
            var chatEntrys = new ObservableCollection<ChatEntryViewModel>()
            {
                new ChatEntryViewModel()
                {
                    User = testUser1,
                    Message = "Test message 1"
                },
                new ChatEntryViewModel()
                {
                    User = testUser2,
                    Message = "Test message 2"
                },
                new ChatEntryViewModel()
                {
                    User = testUser3,
                    Message = "Test message 3"
                },
            };

            // Test available chat rooms
            AvailableChatRooms = new ObservableCollection<ChatRoomViewModel>()
            {
                new ChatRoomViewModel()
                {
                    DisplayName = "Chat room 1",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = activeUser
                },
                new ChatRoomViewModel()
                {
                    DisplayName = "Chat room 2",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = activeUser
                },
                new ChatRoomViewModel()
                {
                    DisplayName = "Chat room 3",
                    Users = users,
                    ChatEntrys = chatEntrys,
                    ActiveUser = activeUser
                }
            };

            var dataService = new DataServiceBase();

            //var thing = dataService.GetChatEntries();
            //var chatRooms = dataService.GetChatRooms();
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
