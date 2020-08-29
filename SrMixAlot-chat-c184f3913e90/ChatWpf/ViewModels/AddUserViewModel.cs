using AutoMapper;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.Models.Dto;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

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
        private ObservableCollection<User> _userResults;

        public AddUserViewModel(IUserAccountService userAccountService, IChatRoomDataService chatRoomDataService)
        {
            _userAccountService = userAccountService;
            _chatRoomDataService = chatRoomDataService;

            SearchForUsersCommand = new RelayCommand<string>(async (username) => await SearchForUsers(username));
            AddUserCommand = new RelayCommand<User>(AddUser);
        }

        /// <summary>
        ///     Command to search for users.
        /// </summary>
        public RelayCommand<string> SearchForUsersCommand { get; }

        /// <summary>
        ///     Command to invite a user.
        /// </summary>
        public RelayCommand<User> AddUserCommand { get; }

        /// <summary>
        ///     Search for users by the specified username search text.
        /// </summary>
        /// <param name="searchText"> The username text to search by. </param>
        private async Task SearchForUsers(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return;

            var users = await _userAccountService.GetUsersWithUsername(searchText);
            var userModels = Mapper.Map<IEnumerable<User>>(users);

            UserResults = new ObservableCollection<User>(userModels);
        }

        /// <summary>
        ///     Invite user to room.
        /// </summary>
        /// <param name="user"> The <see cref="UserDto"/> to invite. </param>
        private void AddUser(User user)
        {
            var userDto = Mapper.Map<UserDto>(user);

            _chatRoomDataService.AddUser(userDto, ChatRoomModel.Id);

            // send message that a user has been added
            MessengerInstance.Send(new NotificationMessage<User>(user, "Add"));
        }

        /// <summary>
        ///     Gets or sets the search result users.
        /// </summary>
        public ObservableCollection<User> UserResults
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
