﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Data.Entities;
using Shared.Models.Models;

namespace Data.Services.Interfaces
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

        /// <summary>
        ///		Create a Jwt bearer token for the user.
        /// </summary>
        /// <param name="user"> User to create token for. </param>
        /// <returns> Jwt bearer token for user. </returns>
		string CreateToken(User user);

        /// <summary>
        ///		Update a user.
        /// </summary>
        /// <param name="userModel"> The <see cref="UserModel"/>. </param>
        /// <returns> True or false to indicate success or failure. </returns>
        bool UpdateUser(UserModel userModel);

        /// <summary>
        ///		Get users matching username search criteria.
        /// </summary>
        /// <param name="username"> The username part to search for. </param>
        /// <returns> Users matching username search criteria. </returns>
		Task<IEnumerable<UserModel>> GetUsersMatchingUsername(string username);
    }
}
