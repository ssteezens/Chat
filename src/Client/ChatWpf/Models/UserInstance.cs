namespace ChatWpf.Models
{
    /// <summary>
    ///		Class for getting/setting the current user instance.
    /// </summary>
    public static class UserInstance
    {
        private static User _currentUser;

        /// <summary>
        ///     The current logged in <see cref="User"/>.
        /// </summary>
        public static User Current
        {
            get => _currentUser;
            set => _currentUser = value;
        }

        /// <summary>
        ///     The display name of the current logged in <see cref="User"/>.
        /// </summary>
        public static string DisplayName => string.IsNullOrEmpty(Current.NickName) ? Current.NickName : Current.Username;
    }
}