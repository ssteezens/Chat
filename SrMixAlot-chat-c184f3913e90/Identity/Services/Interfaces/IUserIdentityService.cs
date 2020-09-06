using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Identity.Services.Interfaces
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public interface IUserIdentityService
	{
		/// <summary>
		///		Adds a user to the database.
		/// </summary>
		/// <param name="registerModel"> Model which contains user and password. </param>
		/// <returns> The created user. </returns>
		Task<IdentityResult> CreateUserAsync(RegisterModel registerModel);

        /// <summary>
        ///		Create a Jwt bearer token for the user.
        /// </summary>
        /// <param name="user"> User to create token for. </param>
        /// <returns> Jwt bearer token for user. </returns>
		string CreateToken(User user);
    }
}
