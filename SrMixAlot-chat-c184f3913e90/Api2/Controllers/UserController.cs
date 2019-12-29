using Api.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;

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
        public IActionResult UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            var userDto = new UserDto()
            {
                Username = User.Identity.Name,
                Nickname = updateUserDto.NickName,
                ProfileImageData = updateUserDto.ProfileImageData
            };
            return Ok(_userService.UpdateUser(userDto));
        }
    }
}
