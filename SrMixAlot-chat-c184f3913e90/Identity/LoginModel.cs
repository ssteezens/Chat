namespace Identity
{
    /// <summary>
    ///     Model used for logging in.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        ///     The username of the login.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     The password of the login.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     Flag to determine if the user wants to remain logged in.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
