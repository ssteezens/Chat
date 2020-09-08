namespace Shared.Models.Models
{
	/// <summary>
    ///		Model for registering a user.
    /// </summary>
    public class RegisterModel
    {
		/// <summary>
        ///		User to register.
        /// </summary>
        public UserModel User { get; set; }

		/// <summary>
        ///		Password for user.
        /// </summary>
        public string Password { get; set; }
    }
}
