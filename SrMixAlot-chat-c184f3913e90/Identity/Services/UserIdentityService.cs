using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Configuration;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    /// <summary>
    ///		Service for getting user related data.
    /// </summary>
    public class UserIdentityService : IUserIdentityService
    {
        private readonly UserManager<User> _userManager;
		private readonly TokenConfiguration _tokenConfiguration;

        public UserIdentityService(UserManager<User> userManager, 
            TokenConfiguration tokenConfiguration)
		{
			_userManager = userManager;
			_tokenConfiguration = tokenConfiguration;
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
