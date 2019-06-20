using Api.Models.Entities;

namespace Api.Models
{
	/// <summary>
    ///		Model for registering a user.
    /// </summary>
    public class RegisterModel
    {
		/// <summary>
        ///		User to register.
        /// </summary>
        public User User { get; set; }

		/// <summary>
        ///		Password for user.
        /// </summary>
        public string Password { get; set; }
    }
}
