using ChatWpf.Models;
using System.Threading.Tasks;

namespace ChatWpf.Services.Data.Interfaces
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
		Task<User> AuthenticateUser(string username, string password);
	}
}
