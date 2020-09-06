using Data.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/User/Update")]
        public IActionResult Update([FromBody] UpdateUserDto updateUserDto)
        {
            var userDto = new UserDto()
            {
                Username = User.Identity.Name,
                Nickname = updateUserDto.NickName,
                ProfileImageData = updateUserDto.ProfileImageData
            };
            return Ok(_userService.UpdateUser(userDto));
        }

        [HttpGet("/User/Search")]
        public async Task<IActionResult> SearchByUsername(string username)
        {
            var users = await _userService.GetUsersMatchingUsername(username);

            return Ok(users);
        }
    }
}
