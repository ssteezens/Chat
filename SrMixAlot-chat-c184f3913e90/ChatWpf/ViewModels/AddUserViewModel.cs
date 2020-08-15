using ChatWpf.Services.Data.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.Models.Dto;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChatWpf.ViewModels
{
    /// <summary>
    ///     View model for adding a user.
    /// </summary>
    public class AddUserViewModel : ViewModelBase
    {
        private readonly IUserAccountService _userAccountService;
        private bool _controlIsOpen;
        private ObservableCollection<UserDto> _userResults;

        public AddUserViewModel(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;

            SearchForUsersCommand = new RelayCommand<string>(async (username) => await SearchForUsers(username));
        }

        /// <summary>
        ///     Command to search for users.
        /// </summary>
        public RelayCommand<string> SearchForUsersCommand { get; }

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
    }
}
