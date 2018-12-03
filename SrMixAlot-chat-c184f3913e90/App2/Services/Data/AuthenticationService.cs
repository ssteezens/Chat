using Chat.Models;
using Chat.Services.Data.Interfaces;
using System;

namespace Chat.Services.Data
{
    /// <summary>
    ///     Service for authenticating a user.
    /// </summary>
    public class AuthenticationService : DataServiceBase, IAuthenticationService
    {
		/// <summary>
		///     Authenticates the user.
		/// </summary>
		/// <param name="username"> The user's username. </param>
		/// <param name="password"> The user's password. </param>
		/// <returns> The authenticated user. </returns>
		public User AuthenticateUser(string username, string password)
		{
			throw new NotImplementedException();
		}
	}
}
