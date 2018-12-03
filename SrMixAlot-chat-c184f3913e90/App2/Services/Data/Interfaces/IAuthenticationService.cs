using Chat.Models;

namespace Chat.Services.Data.Interfaces
{
    /// <summary>
    ///     Interface for authentication service.
    /// </summary>
    public interface IAuthenticationService
	{
        /// <summary>
        ///     Authenticates the user.
        /// </summary>
        /// <param name="username"> The user's username. </param>
        /// <param name="password"> The user's password. </param>
        /// <returns></returns>
		User AuthenticateUser(string username, string password);
	}
}
