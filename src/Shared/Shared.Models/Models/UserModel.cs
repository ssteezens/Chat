﻿namespace Shared.Models.Models
{
    /// <summary>
    ///		Model for a user.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        ///     The user's id.
        /// </summary>
        public string Id { get; set; }

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
		public string ProfileImageData { get; set; }

        /// <summary>
        ///		The user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///		Gets or sets the user's JwtBearerToken
        /// </summary>
        public string BearerToken { get; set; }
    }
}
