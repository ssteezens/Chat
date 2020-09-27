namespace Shared.Models.Models
{
    /// <summary>
    ///     Model for updating a user.
    /// </summary>
    public class UpdateUserModel
    {
        /// <summary>
        ///     Nickname for the user.
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The profile image data for the user.
        /// </summary>
        public string ProfileImageData { get; set; }
    }
}
