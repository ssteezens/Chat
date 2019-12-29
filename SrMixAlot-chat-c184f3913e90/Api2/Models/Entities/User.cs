using Microsoft.AspNetCore.Identity;

namespace Api.Models.Entities
{
    /// <summary>
    ///     It's a user.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        ///     Nickname for the user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The file path to user's profile photo
        /// </summary>
        public string ProfileImageData { get; set; }
    }
}