using Api.Models;
using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.Data
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly ChatContext _context;
		private readonly UserManager<User> _userManager;

		public UserService(ChatContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;
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

		/// <summary>
        ///		Adds a user to the database.
        /// </summary>
        /// <param name="registerModel"> The user to add. </param>
        /// <returns> The created user. </returns>
		public async Task<IdentityResult> CreateUserAsync(RegisterModel registerModel)
		{
			var result = await _userManager.CreateAsync(registerModel.User, registerModel.Password);

			return result;
		}
	}
}
