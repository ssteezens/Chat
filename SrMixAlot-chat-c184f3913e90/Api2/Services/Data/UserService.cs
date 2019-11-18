using Api.Configuration;
using Api.Models;
using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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
		private readonly TokenConfiguration _tokenConfiguration;

		public UserService(ChatContext context, UserManager<User> userManager, TokenConfiguration tokenConfiguration)
		{
			_context = context;
			_userManager = userManager;
			_tokenConfiguration = tokenConfiguration;
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
		
		/// <summary>
        ///		Create a Jwt bearer token for the user.
        /// </summary>
        /// <param name="user"> User to create token for. </param>
        /// <returns> Jwt bearer token for user. </returns>
		public string CreateToken(User user)
		{
			// create token
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Key));
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var token = new JwtSecurityToken(
				_tokenConfiguration.Issuer,
				_tokenConfiguration.Audience,
				claims,
				expires: DateTime.UtcNow.AddMinutes(30),
				signingCredentials: signingCredentials);
			var results = new
			{
				token = new JwtSecurityTokenHandler().WriteToken(token),
				expiration = token.ValidTo
			};

			return results.token;
		}
	}
}
