using GalaSoft.MvvmLight;
using System;

namespace ChatWpf.Models
{
    /// <summary>
    ///     Model for a chat message.
    /// </summary>
    public class ChatMessage : ObservableObject
	{
		private string _message = string.Empty;
        private bool _isSelected;

		/// <summary>
		///     The chat entry's id.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
        ///		The id of the chat room.
        /// </summary>
		public int ChatRoomId { get; set; }

		/// <summary>
		///     The user that sent the chat entry.
		/// </summary>
		public User User { get; set; }

        /// <summary>
        ///     Id of the user.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     Base64 profile image data.
        /// </summary>
        public string ProfileImageData
        {
            get
            {
                var defaultImg = "iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAIAAAAlC+aJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAIiSURBVGhD7Y9bbsQgEARzpT18bpb/PLSFQzdgw8DGiuxSrVYyTTPz9vH++NfeC5ztvcDZ3guc7b3A2d4LnO29wNneCxS+KXa63GUPMG8by69yTS8zdmAX513QyGjd2PVJZ+sYagRrmHSqjomUT4WvivXMuHgBpi7gOGE9M8a7mCWDYWuQyLC2Q7lWXFy2AJO2IZewtn25k5Aj/hLbwaFcSDBmG3IJa9uRCxlyWiby4x1JJxizDbmEtbUkrUigGsoTLYlmMGkNEhnWVpWo4pmfX4081JJoBvMWcJywnqpEFct8yyfOlS3UkpzCyAm+KtZTSk6xzNPfr6SU7bQluRGsoZScYplNOSCr5IGq5Lqx6yYhxTK5fsYNxTImoT7srklIsYxZOeaeYplScm0sX0pOsUxpPcFtxTJViSqWqUpUsUzVZogOxTKl5BTLlJJTLNNyL0eTYhmTkGIZk5BimR1XtnNcw5KbHCuW2fc4Tatimaec1bDkU84UyxzadYFupSeT05O3TI+9d3hBOQzkHIbzQL8D13hH2TkqOewJOHaT15TW95KdhrDDl3lzHdY/auQ+L6/AmgMGK3h/DuuMGW9hiijWFnaqiFnGsZ4Zz1ngG6sKGy9ikCjWFjZYxBRzWGfMSy7A+yuw5oDXW4CX12H9o15sAd5cjb0y5JUW4LXXYG/1e5kFeOeV2IudXmMBXng99m6PF1iA7r/CXj/0ny/w/vgCC8lJCbo5pJsAAAAASUVORK5CYII=";

                if (User != null)
                    return User.ProfileImageData ?? defaultImg;
                
                return defaultImg;
            }
        }

        /// <summary>
        ///     Profile image data in bytes.
        /// </summary>
        public byte[] ProfileImageBytes => Convert.FromBase64String(ProfileImageData);

        /// <summary>
        ///     Indicates if this message is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value, nameof(IsSelected));
        }

		/// <summary>
		///     The text of the chat entry
		/// </summary>
		public string Message
		{
			get => _message;
			set => Set(ref _message, value, nameof(Message));
		}
	}
}
