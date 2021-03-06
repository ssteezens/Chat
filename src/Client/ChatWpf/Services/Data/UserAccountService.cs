﻿using System.Collections.Generic;
using ChatWpf.Models;
using ChatWpf.Services.Data.Interfaces;
using System.Threading.Tasks;
using ServiceStack;
using Shared.Models.Models;
using LoginModel = ChatWpf.Models.LoginModel;
using RegisterModel = ChatWpf.Models.RegisterModel;

namespace ChatWpf.Services.Data
{
    /// <summary>
    ///     Service for authenticating a user.
    /// </summary>
    public class UserAccountService : IUserAccountService
	{
		private readonly IJsonServiceClient _jsonClient;

		public UserAccountService(IJsonServiceClient jsonClient)
		{
			_jsonClient = jsonClient;
		}

		/// <summary>
		///     Authenticates the user.
		/// </summary>
		/// <param name="username"> The user's username. </param>
		/// <param name="password"> The user's password. </param>
		/// <returns> The authenticated user. </returns>
		public async Task<UserModel> LoginUser(string username, string password)
		{
			var loginModel = new LoginModel()
			{
				Username = username,
				Password = password
			};
			var result = await _jsonClient.PostAsync<UserModel>("/api/account/login", loginModel);

			// set bearer token for future requests
			_jsonClient.BearerToken = result.BearerToken;

            return result;
		}

		/// <summary>
        ///		Registers a user.
        /// </summary>
        /// <param name="registerModel"> Model containing the user and password. </param>
        /// <returns> True or false if the registration was successful. </returns>
		public bool RegisterUser(RegisterModel registerModel)
		{
			return _jsonClient.Post<bool>("/api/account/register", registerModel);
		}

        /// <summary>
        ///     Updates a user.
        /// </summary>
        /// <param name="userModel"> The user update model. </param>
        /// <returns> True or false if the operation was successful. </returns>
        public bool UpdateUser(UpdateUserModel userModel)
        {
            return _jsonClient.Post<bool>("/User/Update", userModel);
        }

        /// <summary>
        ///     Finds users with matching usernames.
        /// </summary>
        /// <param name="username"> The username to search for. </param>
        /// <returns> Users matching the username search criteria. </returns>
		public async Task<IEnumerable<UserModel>> GetUsersWithUsername(string username)
        {
            return await _jsonClient.GetAsync<IEnumerable<UserModel>>($"/User/Search?username={username}");
        }
    }
}
