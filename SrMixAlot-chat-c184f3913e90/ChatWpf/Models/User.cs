﻿using GalaSoft.MvvmLight;

namespace ChatWpf.Models
{
	/// <summary>
    ///		Class for a user.
    /// </summary>
    public class User : ObservableObject
    {
		/// <summary>
        ///		Username for the user.
        /// </summary>
		public string Username { get; set; }

        /// <summary>
        ///     Nickname for the user.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The file path to user's profile photo.
        /// </summary>
        public string ImageFilePath { get; set; }

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
	}
}
