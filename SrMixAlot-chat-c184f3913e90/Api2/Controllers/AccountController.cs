using Api.Models;
using Api.Models.Entities;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;
		private readonly IUserService _userService;

		public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager, IUserService userService)
		{
            _logger = logger;
			_signInManager = signInManager;
			_userService = userService;
		}

		/// <summary>
        ///		Controller action for logging in a user.
        /// </summary>
        /// <param name="loginModel"> Model containing login information. </param>
        /// <returns> User data from database, or bad request. </returns>
        [HttpPost("/api/account/login")]
		public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
		{
			var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, false);

			if (result.Succeeded)
			{
				var user = _userService.GetByUsername(loginModel.Username);

				return Ok(user);
			}
			else
			{
				return BadRequest("Could not authenticate user.");
			}
		}

		/// <summary>
        ///		Controller action for registering a user.
        /// </summary>
        /// <param name="registerModel"> Model containing the user and password. </param>
        /// <returns> The created user. </returns>
        [HttpPost("/api/account/register")]
		public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
		{
			var identityResult = await _userService.CreateUserAsync(registerModel);

			return Ok(identityResult == IdentityResult.Success);
		}

		/// <summary>
        ///		Logs out a user.
        /// </summary>
        /// <returns></returns>
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return Ok();
		}
    }
}
