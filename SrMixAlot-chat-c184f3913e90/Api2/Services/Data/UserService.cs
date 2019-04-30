using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.Data.Interfaces;

namespace Api.Services.Data
{
	/// <summary>
	///		Service for getting user related data.
	/// </summary>
    public class UserService : IUserService
    {
        private readonly ChatContext _context;

		public UserService(ChatContext context)
		{
            _context = context;	
		}

		/// <summary>
		///		Gets a user from the database by username.
		/// </summary>
		/// <param name="username"> Username of the user. </param>
		/// <returns> User with username. </returns>
        public User GetByUsername(string username)
		{
			return _context.Users.SingleOrDefault(i => i.UserName == username);
		}
	}
}
