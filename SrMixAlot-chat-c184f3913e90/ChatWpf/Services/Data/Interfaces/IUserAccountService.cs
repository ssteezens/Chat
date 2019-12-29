using ChatWpf.Models;
using Shared.Models.Dto;
using System.Threading.Tasks;

namespace ChatWpf.Services.Data.Interfaces
{
    /// <summary>
    ///     Interface for authentication service.
    /// </summary>
    public interface IUserAccountService
	{
		/// <summary>
		///     Authenticates the user.
		/// </summary>
		/// <param name="username"> The user's username. </param>
		/// <param name="password"> The user's password. </param>
		/// <returns> Task result containing user. </returns>
		Task<UserDto> LoginUser(string username, string password);

        /// <summary>
        ///		Registers a user.
        /// </summary>
        /// <param name="registerModel"> Model containing the user and password. </param>
		/// <returns> True or false if the registration was successful. </returns>
        bool RegisterUser(RegisterModel registerModel);

        /// <summary>
        ///     Updates a user.
        /// </summary>
        /// <param name="userDto"> The user update model. </param>
        /// <returns> True or false if the operation was successful. </returns>
        bool UpdateUser(UpdateUserDto userDto);
    }
}
