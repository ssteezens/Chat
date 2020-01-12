using System.Runtime.InteropServices;
using GalaSoft.MvvmLight;

namespace ChatWpf.Models
{
	/// <summary>
    ///		Class for a user.
    /// </summary>
    public class User : ObservableObject
    {
        private string _nickName;
        private string _profileImageData;

        /// <summary>
        ///     The user's id.
        /// </summary>
        public string Id { get; set; }

		/// <summary>
        ///		Username for the user.
        /// </summary>
		public string Username { get; set; }

        /// <summary>
        ///     Nickname for the user.
        /// </summary>
        public string NickName
        {
            get => _nickName;
            set => Set(ref _nickName, value, nameof(NickName));
        }

        /// <summary>
        ///     The file path to user's profile photo.
        /// </summary>
        public string ProfileImageData
        {
            get => _profileImageData;
            set => Set(ref _profileImageData, value, nameof(ProfileImageData));
        }

		/// <summary>
        ///		The user's email.
        /// </summary>
		public string Email { get; set; }
    }

	/// <summary>
    ///		Class for getting/setting the current user instance.
    /// </summary>
	public static class UserInstance
	{
        private static User _currentUser;

		public static User Current
		{
			get => _currentUser;
            set => _currentUser = value;
		}

        public static string DisplayName => string.IsNullOrEmpty(Current.NickName) ? Current.NickName : Current.Username;
    }
}
