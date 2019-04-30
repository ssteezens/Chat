using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
