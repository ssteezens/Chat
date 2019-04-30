using Api.Models.Entities;

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
	}
}
