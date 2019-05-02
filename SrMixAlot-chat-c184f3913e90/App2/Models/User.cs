using GalaSoft.MvvmLight;

namespace Chat.Models
{
	/// <summary>
    ///		Class for a user.
    /// </summary>
    public class User : ObservableObject
    {
        /// <summary>
        ///     Nickname for the user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The file path to user's profile photo
        /// </summary>
        public string ImageFilePath { get; set; }
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
	}
}
