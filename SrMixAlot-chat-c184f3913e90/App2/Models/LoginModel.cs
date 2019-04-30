namespace Chat.Models
{
	/// <summary>
    ///		Model for logging in a user.
    /// </summary>
    public class LoginModel
    {
		/// <summary>
        ///		Username for login.
        /// </summary>
		public string Username { get; set; }

		/// <summary>
        ///		Password for login.
        /// </summary>
		public string Password { get; set; }

		/// <summary>
        ///		Flag used to keep user logged in.		
        /// </summary>
		public bool RememberMe { get; set; }
    }
}
