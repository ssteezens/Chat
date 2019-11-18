using Api.Models;
using Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Api.Services.Data.Interfaces
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public interface IUserService
	{
		/// <summary>
        ///		Gets a user from the database by username.
        /// </summary>
        /// <param name="username"> Username of the user. </param>
        /// <returns> User with username. </returns>
		User GetByUsername(string username);

		/// <summary>
		///		Adds a user to the database.
		/// </summary>
		/// <param name="registerModel"> Model which contains user and password. </param>
		/// <returns> The created user. </returns>
		Task<IdentityResult> CreateUserAsync(RegisterModel registerModel);

		string CreateToken(User user);
	}
}
