using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<User> _signInManager;

		public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager)
		{
            _logger = logger;
			_signInManager = signInManager;
		}

        [HttpPost("/api/account/login")]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, loginModel.RememberMe, false);

			return Ok();
		}

		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();

			return Ok();
		}
    }
}
