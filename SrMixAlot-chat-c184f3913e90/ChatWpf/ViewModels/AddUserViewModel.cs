using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.Models.Dto;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChatWpf.Models;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///     View model for adding a user.
    /// </summary>
    public class AddUserViewModel : ViewModelBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IChatRoomDataService _chatRoomDataService;
        private bool _controlIsOpen;
        private ObservableCollection<UserDto> _userResults;

        public AddUserViewModel(IUserAccountService userAccountService, IChatRoomDataService chatRoomDataService)
        {
            _userAccountService = userAccountService;
            _chatRoomDataService = chatRoomDataService;

            SearchForUsersCommand = new RelayCommand<string>(async (username) => await SearchForUsers(username));
            AddUserCommand = new RelayCommand<UserDto>(AddUser);
        }

        /// <summary>
        ///     Command to search for users.
        /// </summary>
        public RelayCommand<string> SearchForUsersCommand { get; }

        /// <summary>
        ///     Command to invite a user.
        /// </summary>
        public RelayCommand<UserDto> AddUserCommand { get; }

        /// <summary>
        ///     Search for users by the specified username search text.
        /// </summary>
        /// <param name="searchText"> The username text to search by. </param>
        private async Task SearchForUsers(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return;

            var users = await _userAccountService.GetUsersWithUsername(searchText);

            UserResults = new ObservableCollection<UserDto>(users);
        }

        /// <summary>
        ///     Invite user to room.
        /// </summary>
        /// <param name="user"> The <see cref="UserDto"/> to invite. </param>
        private void AddUser(UserDto user)
        {
            _chatRoomDataService.AddUser(user, ChatRoomModel.Id);
        }

        /// <summary>
        ///     Gets or sets the search result users.
        /// </summary>
        public ObservableCollection<UserDto> UserResults
        {
            get => _userResults;
            set => Set(ref _userResults, value, nameof(UserResults));
        }

        /// <summary>
        ///     Gets or sets whether the control is open.
        /// </summary>
        public bool ControlIsOpen
        {
            get => _controlIsOpen;
            set => Set(ref _controlIsOpen, value, nameof(ControlIsOpen));
        }

        /// <summary>
        ///     The room to add users to.
        /// </summary>
        public ChatRoom ChatRoomModel { get; set; }
    }
}
