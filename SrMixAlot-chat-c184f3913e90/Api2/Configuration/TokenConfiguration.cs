namespace Api.Configuration
{
	/// <summary>
    ///		Configuration for Jwt bearer tokens.
    /// </summary>
    public class TokenConfiguration
	{
		/// <summary>
        ///		JwT encryption key.
        /// </summary>
		public string Key { get; set; } = "alsdkjflasjasldkfjasdlkfjklasjdlafdsjlfsjl";

		/// <summary>
        ///		The issuer of the token. 
        /// </summary>
		public string Issuer { get; set; } = "localhost";

		/// <summary>
        ///		The audience of the token.
        /// </summary>
		public string Audience { get; set; } = "localhost:5000";
	}
}
