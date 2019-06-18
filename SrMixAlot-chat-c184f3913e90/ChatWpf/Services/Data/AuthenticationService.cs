using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using System.Threading.Tasks;

namespace ChatWpf.Services.Data
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
		public Task<User> AuthenticateUser(string username, string password)
		{
			var loginModel = new LoginModel()
			{
				Username = username,
				Password = password
			};
			
            return Client.PostAsync<User>("/api/account/login", loginModel);
		}
	}
}
