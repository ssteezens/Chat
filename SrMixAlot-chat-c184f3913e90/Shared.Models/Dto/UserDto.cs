namespace Shared.Models.Dto
{
    /// <summary>
    ///		DTO object for a user.
    /// </summary>
    public class UserDto
    {
		/// <summary>
        ///		Gets or sets the user's username.
        /// </summary>
		public string Username { get; set; }

		/// <summary>
        ///		Gets or sets the user's nickname.
        /// </summary>
		public string Nickname { get; set; }

		/// <summary>
        ///		Gets or sets the user's image file path.
        /// </summary>
		public string ImageFilePath { get; set; }
    }
}
