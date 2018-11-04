namespace Api.Models
{
    /// <summary>
    ///     It's a user.
    /// </summary>
    public class User
    {
        /// <summary>
        ///     The user's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nickname for the user
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        ///     The file path to user's profile photo
        /// </summary>
        public string ImageFilePath { get; set; }
    }
}