﻿using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using ServiceStack;

namespace ChatWpf.Services.Data
{
    /// <summary>
    ///     Service for authenticating a user.
    /// </summary>
    public class UserAccountService : DataServiceBase, IUserAccountService
    {
		/// <summary>
		///     Authenticates the user.
		/// </summary>
		/// <param name="username"> The user's username. </param>
		/// <param name="password"> The user's password. </param>
		/// <returns> The authenticated user. </returns>
		public User LoginUser(string username, string password)
		{
			var loginModel = new LoginModel()
			{
				Username = username,
				Password = password
			};
			
            return Client.Post<User>("/api/account/login", loginModel);
		}

		/// <summary>
        ///		Registers a user.
        /// </summary>
        /// <param name="registerModel"> Model containing the user and password. </param>
        /// <returns> True or false if the registration was successful. </returns>
		public bool RegisterUser(RegisterModel registerModel)
		{
			return Client.Post<bool>("/api/account/register", registerModel);
		}
	}
}