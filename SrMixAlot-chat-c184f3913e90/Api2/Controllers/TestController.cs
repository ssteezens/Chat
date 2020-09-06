using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : Controller
    {
        private readonly ChatContext _chatContext;

		public TestController(ChatContext chatContext)
		{
			_chatContext = chatContext;
		}

		[HttpGet("/api/test")]
		public IActionResult Test()
		{
			var username = User.Identity.Name;

			var thing = _chatContext.ChatRooms
				.Include(room => room.ChatMessages)
				.Include(room => room.Users)
				.ToList();

			return Ok(thing);
		}
    }
}
