using System;
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
        ///		Gets or sets the user's image file path.
        /// </summary>
        public string ProfileImageData
        {
            get => string.IsNullOrEmpty(_profileImageData)
                ? "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIiSURBVGhD7Y9bbsQgEARzpT18bpb/PLSFQzdgw8DGiuxSrVYyTTPz9vH++NfeC5ztvcDZ3guc7b3A2d4LnO29wNneCxS+KXa63GUPMG8by69yTS8zdmAX513QyGjd2PVJZ+sYagRrmHSqjomUT4WvivXMuHgBpi7gOGE9M8a7mCWDYWuQyLC2Q7lWXFy2AJO2IZewtn25k5Aj/hLbwaFcSDBmG3IJa9uRCxlyWiby4x1JJxizDbmEtbUkrUigGsoTLYlmMGkNEhnWVpWo4pmfX4081JJoBvMWcJywnqpEFct8yyfOlS3UkpzCyAm+KtZTSk6xzNPfr6SU7bQluRGsoZScYplNOSCr5IGq5Lqx6yYhxTK5fsYNxTImoT7srklIsYxZOeaeYplScm0sX0pOsUxpPcFtxTJViSqWqUpUsUzVZogOxTKl5BTLlJJTLNNyL0eTYhmTkGIZk5BimR1XtnNcw5KbHCuW2fc4Tatimaec1bDkU84UyxzadYFupSeT05O3TI+9d3hBOQzkHIbzQL8D13hH2TkqOewJOHaT15TW95KdhrDDl3lzHdY/auQ+L6/AmgMGK3h/DuuMGW9hiijWFnaqiFnGsZ4Zz1ngG6sKGy9ikCjWFjZYxBRzWGfMSy7A+yuw5oDXW4CX12H9o15sAd5cjb0y5JUW4LXXYG/1e5kFeOeV2IudXmMBXng99m6PF1iA7r/CXj/0ny/w/vgCC8lJCbo5pJsAAAAASUVORK5CYII="
                : _profileImageData;
            set => _profileImageData = value;
        }

        /// <summary>
        ///     Profile image data in bytes.
        /// </summary>
        public byte[] ProfileImageBytes => Convert.FromBase64String(ProfileImageData);

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
